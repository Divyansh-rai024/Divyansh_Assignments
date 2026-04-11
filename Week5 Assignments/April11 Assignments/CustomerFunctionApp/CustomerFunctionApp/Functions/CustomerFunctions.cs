using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using CustomerFunctionApp.Data;
using CustomerFunctionApp.Models;
using System.Text.Json;

namespace CustomerFunctionApp.Functions
{
    public class CustomerFunctions
    {
        private readonly AppDbContext _context;

        public CustomerFunctions(AppDbContext context)
        {
            _context = context;
        }

        [Function("GetCustomers")]
        public async Task<HttpResponseData> GetCustomers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers")] HttpRequestData req)
        {
            var customers = await _context.Customers.ToListAsync();

            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteAsJsonAsync(customers);
            return response;
        }

        [Function("GetCustomerById")]
        public async Task<HttpResponseData> GetCustomerById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers/{id:int}")] HttpRequestData req,
            int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            var response = req.CreateResponse(
                customer == null ? System.Net.HttpStatusCode.NotFound : System.Net.HttpStatusCode.OK);

            await response.WriteAsJsonAsync(customer);
            return response;
        }

        [Function("CreateCustomer")]
        public async Task<HttpResponseData> CreateCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "customers")] HttpRequestData req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var customer = JsonSerializer.Deserialize<Customer>(body);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var response = req.CreateResponse(System.Net.HttpStatusCode.Created);
            await response.WriteAsJsonAsync(customer);
            return response;
        }

        [Function("UpdateCustomer")]
        public async Task<HttpResponseData> UpdateCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "customers/{id:int}")] HttpRequestData req,
            int id)
        {
            var existing = await _context.Customers.FindAsync(id);
            if (existing == null)
            {
                return req.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonSerializer.Deserialize<Customer>(body);

            existing.Name = updated.Name;
            existing.Email = updated.Email;
            existing.Phone = updated.Phone;

            await _context.SaveChangesAsync();

            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteAsJsonAsync(existing);
            return response;
        }

        [Function("DeleteCustomer")]
        public async Task<HttpResponseData> DeleteCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "customers/{id:int}")] HttpRequestData req,
            int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return req.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return req.CreateResponse(System.Net.HttpStatusCode.NoContent);
        }
    }
}