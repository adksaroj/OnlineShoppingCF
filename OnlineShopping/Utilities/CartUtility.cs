using OnlineShopping.Models;
using OnlineShoppingDataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.Utilities
{
    public static class CartUtility
    {
        public static List<CartViewModel> GetCartItemForUser(string emailId)
        {
            var cartData = HttpContext.Current.Session["cart"];

            var allCart = (List<CartViewModel>)cartData;

            var cartItemsForUser = allCart?.Where(cvm => cvm.user.Email == emailId).ToList();

            return cartItemsForUser;
        }

        public static bool IsProductAlreadyOnCart(Product product, string emailId)
        {
            var userCart = GetCartItemForUser(emailId);

            var productFromCart = userCart?.Where(cvm => cvm.product.Id == product.Id).FirstOrDefault();

            if (productFromCart != null)
            {
                return true;
            }

            return false;
        }
    }
}