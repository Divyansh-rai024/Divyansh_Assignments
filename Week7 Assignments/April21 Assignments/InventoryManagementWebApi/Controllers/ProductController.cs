using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/product")]

public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    } 
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(_service.GetAllProducts());
    }
     
    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        var result = _service.AddProduct(product);
        return Ok(new { message = result });
    }
     
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, Product product)
    {
        var result = _service.UpdateProduct(id, product);

        if (result == "Product Not Found")
            return NotFound();

        return Ok(new { message = result });
    }
     
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var result = _service.DeleteProduct(id);

        if (result == "Product Not Found")
            return NotFound();

        return Ok(new { message = result });
    }
}