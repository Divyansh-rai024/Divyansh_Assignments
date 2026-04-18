using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderItem
{
    [Key]
    public int OrderItemID { get; set; }

    [ForeignKey("Order")]
    public int OrderID { get; set; }

    [ForeignKey("Product")]
    public int ProductID { get; set; }

    public int Quantity { get; set; }

    public Product? Product { get; set; }
    public Order? Order { get; set; }
}