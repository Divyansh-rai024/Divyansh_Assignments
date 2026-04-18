using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/notification")]
public class NotificationController : ControllerBase
{
    private readonly NotificationService _service;

    public NotificationController(NotificationService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [Authorize(Roles = "InventoryManager")]
    [HttpPost]
    public IActionResult Add(Notification notification)
    {
        var result = _service.Add(notification);

        if (result == "User Not Found")
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }
}