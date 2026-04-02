using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstEFinASP.netcore.Models
{
    public class Product
    {
        public int ProductID {  get; set; }

        [Required]
        public string ProductName { get; set; }

        [Display(Name ="Who buyed")]
        [ForeignKey("CustomerID")]
        public int CustomerID {  get; set; }

        public Customer Customer { get; set; }



    }
}
