using OnlineShopping.Models;
using OnlineShopping.Utilities;
using OnlineShoppingDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        //[ChildActionOnly]
        public ActionResult Create()
        {
            if (ModelState.IsValid)
            {
                using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
                {
                    var cart = CartUtility.GetCartItemForUser(User.Identity.Name);

                    if (cart != null)
                    {
                        var order = new Order();
                        order.ClientId = UserUtility.GetUserByUserId(User.Identity.Name).Id;
                        order.Quantity = 1;
                        order.Date = DateTime.Now;
                        order.OrderStatus = "Ordered";
                        order.Price = 500;

                        dbContext.Orders.Add(order);
                        dbContext.SaveChanges();
                    }
                }
                return RedirectToAction("myorders");

            }


            return RedirectToAction("checkout");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateV2()
        {
            if (ModelState.IsValid)
            {
                using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
                {
                    var currentUserId = UserUtility.GetUserByUserId(User.Identity.Name).Id;
                    var cart = dbContext.
                                        Carts.
                                        Where(x => x.UserId == currentUserId).
                                        SelectMany(p => p.Products).ToList();

                    if (cart != null)
                    {
                        var order = new Order();
                        order.ClientId = UserUtility.GetUserByUserId(User.Identity.Name).Id;
                        order.Quantity = 1;
                        order.Date = DateTime.Now;
                        order.OrderStatus = "Ordered";
                        order.Price = 500;

                        foreach (var item in cart)
                        {
                            order.Products.Add(item);
                        }

                        dbContext.Orders.Add(order);
                        dbContext.SaveChanges();
                    }
                }
                return RedirectToAction("myorders");

            }
            return View();
        }

        public ActionResult MyOrders()
        {
            using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
            {
                var loggedInUserId = UserUtility.GetUserByUserId(User.Identity.Name).Id;
                var orders = dbContext.Orders.Where(o => o.ClientId == loggedInUserId).ToList();
                if (orders.Count > 0)
                {
                    List<OrderViewModel> ordersListVM = new List<OrderViewModel>();

                    foreach (var order in orders)
                    {
                        OrderViewModel orderVM = new OrderViewModel();
                        List<ProductViewModel> productListVM = new List<ProductViewModel>();

                        if (order.Products != null)
                        {
                            foreach (var product in order.Products)
                            {
                                ProductViewModel productView = new ProductViewModel();
                                productView.Id = product.Id;
                                productView.ProductId = product.ProductId;
                                productView.ProductName = product.ProductName;
                                productView.Category = product.Category;
                                productView.Description = product.Description;
                                productView.Cost = (decimal)product.Cost;
                                productView.Quantity = (int)order.Quantity;
                                productListVM.Add(productView);
                            }
                        }

                        //pvm.Id = order.Products.Id;
                        //pvm.ProductId = order.Products.ProductId;
                        //pvm.ProductName = order.Products.ProductName;
                        //pvm.Category = order.Products.Category;
                        //pvm.Description = order.Products.Description;
                        //pvm.Quantity = (int)order.Quantity;

                        List<ProductViewModel> productsForOrder = new List<ProductViewModel>();
                        productsForOrder.AddRange(productListVM);

                        orderVM.OrderId = order.Id;
                        orderVM.Products = productsForOrder;
                        orderVM.OrderTotal = (decimal)order.Price;
                        orderVM.PaymentMode = "Cash on Delivery";
                        orderVM.OrderAddress = order.User.Address;
                        orderVM.Products = productsForOrder;
                        orderVM.OrderStatus = order.OrderStatus;

                        ordersListVM.Add(orderVM);

                    }

                    return View(ordersListVM);

                }
            }

            return View();
        }
    }
}