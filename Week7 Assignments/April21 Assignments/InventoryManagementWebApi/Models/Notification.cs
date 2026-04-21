using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Notification
{
    [Key]
    public int NotificationId { get; set; }

    [ForeignKey("User")]
    public long UserId { get; set; }

    public string Message { get; set; }

    public string ProductName { get; set; }

    public int Quantity { get; set; }

    public DateTime DateCreated { get; set; }

    public User? User { get; set; }
}