using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    public int OrderID { get; set; }

    [ForeignKey("Customer")]
    public int CustomerID { get; set; }

    public Customer? Customer { get; set; }

    public DateTime OrderDate { get; set; }

    public string? Status { get; set; }

    public decimal TotalAmount { get; set; }

    public List<OrderItem>? OrderItems { get; set; }

    public List<Product>? Products { get; set; }
}