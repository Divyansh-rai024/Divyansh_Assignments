using System.ComponentModel.DataAnnotations;

namespace CodeFirstEFinASP.netcore.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="please enter your first name")]
        public string FirstName {  get; set; }

        [Required(ErrorMessage = "please enter your last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "please enter your Email")]
        [EmailAddress(ErrorMessage ="enter a valid email")]
        public string Email {  get; set; }

        [Required(ErrorMessage ="Enter your age")]
        [Range(0,100,ErrorMessage ="Please enter your age between 1-100 only")]
        public int Age {  get; set; }


    }
}
