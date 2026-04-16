using Microsoft.AspNetCore.Mvc;
using QrSecureApi.Models;

[ApiController]
[Route("api/[controller]")]
public class QrController : ControllerBase
{
    private readonly CosmosDbService _cosmos;
    private readonly KeyVaultService _kv;

    private readonly string baseUrl = "h";

    public QrController(CosmosDbService cosmos, KeyVaultService kv)
    {
        _cosmos = cosmos;
        _kv = kv;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] string token)
    {
        if (string.IsNullOrEmpty(token))
            return BadRequest("Token cannot be empty");

        var encrypted = await _kv.EncryptAsync(token);

        var item = new QrItem
        {
            id = Guid.NewGuid().ToString(),
            EncryptedValue = encrypted
        };

        await _cosmos.SaveAsync(item);

        var qr = QrService.Generate($"{baseUrl}/api/Qr/scan/{item.id}");

        return File(qr, "image/png");
    }

    [HttpGet("scan/{id}")]
    public async Task<IActionResult> Scan(string id)
    {
        var item = await _cosmos.GetAsync(id);

        if (item == null || string.IsNullOrEmpty(item.EncryptedValue))
            return NotFound();

        var decrypted = await _kv.DecryptAsync(item.EncryptedValue);

        return Content($"<h2>Decrypted Value:</h2><p>{decrypted}</p>", "text/html");
    }

    [HttpGet("login-qr")]
    public async Task<IActionResult> GenerateLoginQr()
    {
        var session = new LoginSession
        {
            id = Guid.NewGuid().ToString(),
            IsLoggedIn = false
        };

        await _cosmos.SaveSessionAsync(session);

        var qr = QrService.Generate($"{baseUrl}/api/Qr/login/{session.id}");

        return File(qr, "image/png");
    }

    [HttpGet("login/{id}")]
    public async Task<IActionResult> Login(string id)
    {
        var session = await _cosmos.GetSessionAsync(id);

        if (session == null)
            return NotFound();

        session.IsLoggedIn = true;
        session.Token = Guid.NewGuid().ToString();

        await _cosmos.UpdateSessionAsync(session);

        return Redirect($"{baseUrl}/api/Qr/data/{session.Token}");
    }

    [HttpGet("status/{id}")]
    public async Task<IActionResult> Status(string id)
    {
        var session = await _cosmos.GetSessionAsync(id);

        if (session == null)
            return NotFound();

        return Ok(session);
    }

    [HttpGet("data/{token}")]
    public async Task<IActionResult> GetData(string token)
    {
        var session = await _cosmos.GetSessionByTokenAsync(token);

        if (session == null || !session.IsLoggedIn)
            return Unauthorized();

        var data = await _cosmos.GetAllItemsAsync();

        var html = @"
<html>
<head>
<style>
body { font-family: Arial; padding:20px; }
table { width:100%; border-collapse: collapse; margin-top:10px; }
th, td { border:1px solid #ccc; padding:10px; }
th { background:#f2f2f2; }
input { margin:5px; padding:5px; }
button { padding:5px 10px; }
form { display:inline; }
</style>
</head>
<body>

<h2>Items</h2>

<h3>Create Item</h3>
<form method='post' action='/api/Qr/create-form'>
<input name='name' placeholder='Name' required />
<input name='description' placeholder='Description' required />
<input name='category' placeholder='Category' required />
<button type='submit'>Create</button>
</form>

<table>
<thead>
<tr>
<th>Name</th>
<th>Description</th>
<th>Category</th>
<th>Actions</th>
</tr>
</thead>
<tbody>
";

        if (data != null)
        {
            foreach (var item in data)
            {
                html += "<tr>";
                html += "<td>" + item.name + "</td>";
                html += "<td>" + item.description + "</td>";
                html += "<td>" + item.category + "</td>";
                html += "<td>";

                html += "<a href='/api/Qr/details/" + item.id + "'>Details</a> ";

                html += "<form method='post' action='/api/Qr/delete-form/" + item.id + "'>";
                html += "<button type='submit'>Delete</button>";
                html += "</form>";

                html += "</td>";
                html += "</tr>";
            }
        }

        html += @"
</tbody>
</table>

</body>
</html>";

        return Content(html, "text/html");
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(string id)
    {
        var item = await _cosmos.GetAsync(id);

        if (item == null)
            return NotFound();

        var html = "<h2>Details</h2>";
        html += "<p><b>Name:</b> " + item.name + "</p>";
        html += "<p><b>Description:</b> " + item.description + "</p>";
        html += "<p><b>Category:</b> " + item.category + "</p>";

        return Content(html, "text/html");
    }

    [HttpPost("create-form")]
    public async Task<IActionResult> CreateForm([FromForm] Product product)
    {
        product.id = Guid.NewGuid().ToString();
        await _cosmos.CreateItemAsync(product);

        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpPost("delete-form/{id}")]
    public async Task<IActionResult> DeleteForm(string id)
    {
        await _cosmos.DeleteItemAsync(id);

        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        product.id = Guid.NewGuid().ToString();
        await _cosmos.CreateItemAsync(product);
        return Ok(product);
    }

    [HttpPut("edit")]
    public async Task<IActionResult> Edit([FromBody] Product product)
    {
        await _cosmos.UpdateItemAsync(product);
        return Ok(product);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _cosmos.DeleteItemAsync(id);
        return Ok("Deleted");
    }
}