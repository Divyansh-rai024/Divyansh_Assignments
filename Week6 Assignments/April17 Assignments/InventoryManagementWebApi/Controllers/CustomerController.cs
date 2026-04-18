using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _service;

    public CustomerController(CustomerService service)
    {
        _service = service;
    }


    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        return Ok(_service.GetAllCustomers());
    }


    [HttpPost]
    public IActionResult AddCustomer(Customer customer)
    {
        var result = _service.AddCustomer(customer);

        if (result == "Customer already exists")
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }


    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, Customer customer)
    {
        var result = _service.UpdateCustomer(id, customer);

        if (result == "Customer Not Found")
            return NotFound();

        return Ok(new { message = result });
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        var result = _service.DeleteCustomer(id);

        if (result == "Customer Not Found")
            return NotFound();

        if (result.Contains("Cannot delete"))
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }
}