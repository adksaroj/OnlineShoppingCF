using OnlineShopping.Models;
using OnlineShopping.Utilities;
using OnlineShoppingDataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cartItemsForUser = CartUtility.GetCartItemForUser(User.Identity.Name);
            return View(cartItemsForUser);
        }

        //[HttpPost]
        public ActionResult AddToCart(int id)
        {
            var cartData = CartUtility.GetCartItemForUser(User.Identity.Name);

            var dbProd = new Product();

            var newCart = new CartViewModel();
            newCart.user = new User() { Email = User.Identity.Name };

            using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
            {
                dbProd = dbContext.Products.Where(p => p.Id == id).FirstOrDefault();
            }

            //if (cartData != null && product != null)
            //{
            //    dbProd.Id = product.Id;
            //    dbProd.ProductId = product.ProductId;
            //    dbProd.ProductName = product.ProductName;
            //    dbProd.Category = product.Category;
            //    dbProd.Cost = product.Cost;
            //    dbProd.Description = product.Description;
            //    dbProd.ImageName = product.Image.FileName;
            //}
            if (dbProd.Id == 0 && cartData == null)
            {

            }
            //return Json("Could not add product");
            else if (dbProd.Id != 0 && cartData != null)
            {
                if (CartUtility.IsProductAlreadyOnCart(dbProd, User.Identity.Name)) //User.Identity.Name = emailId
                {
                    //find the product existing on cart and increment its quantity
                    cartData.Find(cvm => cvm.product.Id == dbProd.Id).Quantity++;
                }
                else
                {
                    newCart.product = dbProd;
                    cartData.Add(newCart);
                }
                Session["cart"] = cartData;
            }
            else if (dbProd.Id != 0 && cartData == null)
            {
                var cartList = new List<CartViewModel>();
                newCart.product = dbProd;

                if (CartUtility.IsProductAlreadyOnCart(dbProd, User.Identity.Name)) //User.Identity.Name = emailId
                {
                    newCart.Quantity++;
                }

                cartList.Add(newCart);
                Session["cart"] = cartList;
            }

            return RedirectToAction("index");
        }

        public ActionResult Remove(int id)
        {
            //get all products on cart from session storage
            var allCartData = (List<CartViewModel>)Session["cart"];


            //remove only those items with matching product and user ids
            allCartData.RemoveAll(cvm => cvm.product.Id == id && cvm.user.Email == User.Identity.Name);

            //update session storage new list
            Session["cart"] = allCartData;

            return RedirectToAction("index");
        }

        public ActionResult MyCart()
        {
            List<Product> productsOnCart = null;

            using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
            {
                var currentUserId = UserUtility.GetUserByUserId(User.Identity.Name).Id;
                var cartdata = dbContext.
                                        Carts.
                                        Where(x => x.UserId == currentUserId);

                productsOnCart = cartdata.SelectMany(p => p.Products).ToList();
            }
            return View(productsOnCart);
        }

        //Add to Cart
        //[HttpPost]
        public async Task<ActionResult> Add(int id)
        {
            if (ModelState.IsValid)
            {
                using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
                {
                    var cart = new Cart();

                    cart.UserId = UserUtility.GetUserByUserId(User.Identity.Name).Id;
                    var prodcut = dbContext.Products.Find(id);
                    cart.Products.Add(prodcut);

                    dbContext.Carts.Add(cart);
                    await dbContext.SaveChangesAsync();

                    return RedirectToAction("mycart");
                }
            }
            return View();
        }
    }
}