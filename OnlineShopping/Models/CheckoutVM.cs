using OnlineShoppingDataAccess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class CheckoutVM
    {
        [Required]
        public List<CartViewModel> Cart { get; set; }

        public User User { get; set; }

        [Required]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        public decimal OrderTotal { get; set; }

        public CheckoutVM()
        {
            Cart = new List<CartViewModel>();
        }
    }
}