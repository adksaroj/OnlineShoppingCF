using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}