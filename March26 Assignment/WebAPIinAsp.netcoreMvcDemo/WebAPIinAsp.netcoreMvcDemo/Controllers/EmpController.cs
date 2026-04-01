using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIinAsp.netcoreMvcDemo.Models;
using WebAPIinAsp.netcoreMvcDemo;

namespace WebApiInAsp.netcoreMvcDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly IEmployee _employeeService;

        public EmpController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        private string GetFullImageUrl(string? path)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            if (string.IsNullOrEmpty(path))
                return baseUrl + "/uploads/default.jpeg";

            // prevent double URL
            if (path.StartsWith("http"))
                return path;

            return baseUrl + path;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAll(int page = 1, int pageSize = 5)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(page, pageSize);

            foreach (var emp in employees)
            {
                emp.ImagePath = GetFullImageUrl(emp.ImagePath);
            }

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound("Employee not found");

            employee.ImagePath = GetFullImageUrl(employee.ImagePath);

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Create([FromForm] Employee employee, IFormFile image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var added = await _employeeService.AddEmployeeAsync(employee, image);

            added.ImagePath = GetFullImageUrl(added.ImagePath);

            return Ok(added);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Update(
            int id,
            [FromForm] EmployeeUpdateDto employeeDto,
            IFormFile? image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new Employee
            {
                Id = id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Age = employeeDto.Age,
                ImagePath = employeeDto.ImagePath
            };

            var updated = await _employeeService.UpdateEmployeeAsync(employee, image);

            if (updated == null)
                return NotFound("Employee not found to update");

            updated.ImagePath = GetFullImageUrl(updated.ImagePath);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);

            if (deleted == null)
                return NotFound("Employee not found to delete");

            deleted.ImagePath = GetFullImageUrl(deleted.ImagePath);

            return Ok(deleted);
        }
    }
}