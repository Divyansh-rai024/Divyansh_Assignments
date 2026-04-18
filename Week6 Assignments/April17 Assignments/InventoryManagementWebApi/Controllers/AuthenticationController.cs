using InventoryManagementWebApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]

/*
 * {
  "email": "admin@gmail.com",
  "password": "123",
  "username": "admin",
  "mobileNumber": "9999999999",
  "userRole": "Admin"
}

{
  "email": "manager@gmail.com",
  "password": "123",
  "username": "manager",
  "mobileNumber": "8888888888",
  "userRole": "InventoryManager"
}

*/



public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthenticationController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("register")]
    public IActionResult Register(User user)
    {
        var result = _auth.Register(user);

        if (result.Contains("exists"))
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginModel model)
    {
        var token = _auth.Login(model);

        if (token == null)
            return Unauthorized();

        return Ok(new { token });
    }
}