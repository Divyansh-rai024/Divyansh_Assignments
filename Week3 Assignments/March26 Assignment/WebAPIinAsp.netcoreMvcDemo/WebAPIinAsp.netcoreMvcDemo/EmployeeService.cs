using Microsoft.EntityFrameworkCore;
using WebAPIinAsp.netcoreMvcDemo.Models;
using System.IO;

namespace WebAPIinAsp.netcoreMvcDemo
{
    public class EmployeeService : IEmployee
    {
        private readonly EmpContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeService(
            EmpContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                employee.ImagePath = await SaveImageAsync(image);
            }
            else
            {
                employee.ImagePath = "/uploads/default.jpeg";
            }

            await _context.employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null) return null;

            DeleteImageFile(employee.ImagePath);

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            return await _context.employees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.employees.FindAsync(id);
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee employee, IFormFile? image)
        {
            var existing = await _context.employees.FindAsync(employee.Id);
            if (existing == null) return null;

            existing.FirstName = employee.FirstName;
            existing.LastName = employee.LastName;
            existing.Email = employee.Email;
            existing.Age = employee.Age;

            if (image != null && image.Length > 0)
            {
                DeleteImageFile(existing.ImagePath);
                existing.ImagePath = await SaveImageAsync(image);
            }
            // if no image → keep existing image (including default)

            await _context.SaveChangesAsync();

            return existing;
        }

        private void DeleteImageFile(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || imagePath.Contains("default.jpeg"))
                return;

            var fullPath = Path.Combine(
                _env.WebRootPath ?? "wwwroot",
                imagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar)
            );

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(uploadsFolder, imageName);

            using var stream = new FileStream(imagePath, FileMode.Create);
            await image.CopyToAsync(stream);

            return "/uploads/" + imageName;
        }
    }
}