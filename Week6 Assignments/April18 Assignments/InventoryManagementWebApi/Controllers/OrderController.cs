using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService _service;

    public OrderController(OrderService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(_service.GetOrders());
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public IActionResult GetOrderById(int id)
    {
        var order = _service.GetOrderById(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("customer/{customerId}")]
    public IActionResult GetByCustomer(int customerId)
    {
        return Ok(_service.GetOrderByCustomerId(customerId));
    }

    [Authorize(Roles = "InventoryManager")]
    [HttpPost]
    public IActionResult PlaceOrder(Order order)
    {
        return Ok(_service.PlaceOrder(order));
    }

    [Authorize(Roles = "InventoryManager")]
    [HttpPost("{orderId}/items")]
    public IActionResult AddItem(int orderId, OrderItem item)
    {
        var result = _service.AddOrderItem(orderId, item);

        if (result == "Order Not Found" || result == "Product Not Found")
            return NotFound();

        if (result == "Insufficient Stock")
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, Order order)
    {
        var result = _service.UpdateOrder(id, order);

        if (result == "Order Not Found")
            return NotFound();

        return Ok(new { message = result });
    }

    [Authorize(Roles = "InventoryManager")]
    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        var result = _service.DeleteOrder(id);

        if (result == "Order Not Found")
            return NotFound();

        if (result.Contains("Cannot"))
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }
}