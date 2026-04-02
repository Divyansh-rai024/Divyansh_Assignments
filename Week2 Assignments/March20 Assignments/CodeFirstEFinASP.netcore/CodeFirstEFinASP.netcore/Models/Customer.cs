using System.ComponentModel.DataAnnotations;

namespace CodeFirstEFinASP.netcore.Models
{
    public class Customer
    {
        public int CustomerID {  get; set; }
        [Required]
        public string CustomerName { get; set; }
        public  ICollection<Product> Products {  get; set; }


    }
}
