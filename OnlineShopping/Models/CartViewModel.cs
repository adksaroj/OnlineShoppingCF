using OnlineShoppingDataAccess;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class CartViewModel
    {
        public User user { get; set; }

        public Product product { get; set; }

        [Range(1, 50, ErrorMessage = "Quantity must be between 1 and 50")]
        [Required(ErrorMessage = "Quantity is required and must be between 1 and 50")]
        public int Quantity { get; set; }

        public CartViewModel()
        {
            Quantity = 1;
        }
    }
}