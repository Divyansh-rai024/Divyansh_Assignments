using System.Collections.Generic;
using System.Linq;

public class CustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }
     
    public List<Customer> GetAllCustomers()
    {
        return _context.Customers.ToList();
    }
     
    public string AddCustomer(Customer customer)
    { 
        var exists = _context.Customers
            .Any(c => c.Email == customer.Email);

        if (exists)
            return "Customer already exists";

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return "Customer Added Successfully";
    }
     
    public string UpdateCustomer(int id, Customer updated)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
            return "Customer Not Found";

        customer.CustomerName = updated.CustomerName;
        customer.Email = updated.Email;
        customer.MobileNumber = updated.MobileNumber;

        _context.SaveChanges();

        return "Customer Updated Successfully";
    }
     
    public string DeleteCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
            return "Customer Not Found";
         
        var hasOrders = _context.Orders
            .Any(o => o.CustomerID == id);

        if (hasOrders)
            return "Cannot delete customer with existing orders";

        _context.Customers.Remove(customer);
        _context.SaveChanges();

        return "Customer Deleted Successfully";
    }
}