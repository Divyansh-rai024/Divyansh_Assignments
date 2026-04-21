using System.Collections.Generic;
using System.Linq;

public class OrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Order> GetOrders()
    {
        return _context.Orders.ToList();
    }

    public Order GetOrderById(int id)
    {
        return _context.Orders.FirstOrDefault(o => o.OrderID == id);
    }

    public List<Order> GetOrderByCustomerId(int customerId)
    {
        return _context.Orders.Where(o => o.CustomerID == customerId).ToList();
    }

    public Order PlaceOrder(Order order)
    {
        order.OrderDate = DateTime.Now;
        order.Status = "Pending";
        order.TotalAmount = 0;

        _context.Orders.Add(order);
        _context.SaveChanges();

        return order;
    }

    public string AddOrderItem(int orderId, OrderItem item)
    {
        var order = _context.Orders.Find(orderId);
        if (order == null) return "Order Not Found";

        var product = _context.Products.Find(item.ProductID);
        if (product == null) return "Product Not Found";

        if (product.Quantity < item.Quantity)
            return "Insufficient Stock";

        product.Quantity -= item.Quantity;

        item.OrderID = orderId;

        _context.OrderItems.Add(item);
        _context.SaveChanges();

        var items = _context.OrderItems.Where(i => i.OrderID == orderId).ToList();

        decimal total = 0;
        foreach (var i in items)
        {
            var p = _context.Products.Find(i.ProductID);
            total += p.Price * i.Quantity;
        }

        order.TotalAmount = total;
        _context.SaveChanges();

        return "Item Added";
    }

    public string UpdateOrder(int id, Order updated)
    {
        var order = _context.Orders.Find(id);
        if (order == null) return "Order Not Found";

        order.Status = updated.Status;
        _context.SaveChanges();

        return "Order Updated";
    }

    public string DeleteOrder(int id)
    {
        var order = _context.Orders.Find(id);
        if (order == null) return "Order Not Found";

        if (order.Status != "Pending")
            return "Cannot cancel non-pending order";

        var items = _context.OrderItems.Where(i => i.OrderID == id).ToList();

        foreach (var item in items)
        {
            var product = _context.Products.Find(item.ProductID);
            if (product != null)
                product.Quantity += item.Quantity;
        }

        _context.OrderItems.RemoveRange(items);
        _context.Orders.Remove(order);
        _context.SaveChanges();

        return "Order Cancelled";
    }
}