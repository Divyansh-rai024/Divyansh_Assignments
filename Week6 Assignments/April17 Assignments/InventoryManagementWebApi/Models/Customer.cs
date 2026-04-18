using System.ComponentModel.DataAnnotations;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    public string CustomerName { get; set; }

    public string MobileNumber { get; set; }

    [Required]
    public string Email { get; set; }

    // One-to-Many with Orders
    public List<Order>? Orders { get; set; }
}