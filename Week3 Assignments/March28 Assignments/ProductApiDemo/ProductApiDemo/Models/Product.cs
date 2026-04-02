using System.ComponentModel.DataAnnotations;

namespace ProductApiDemo.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public decimal Price { get; set; }

        public string? Category { get; set; }
    }
}