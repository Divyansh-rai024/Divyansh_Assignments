using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public long UserId { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Username { get; set; }

    public string MobileNumber { get; set; }

    [Required]
    public string UserRole { get; set; } // Admin / InventoryManager
}