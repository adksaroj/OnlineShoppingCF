using OnlineShoppingDataAccess;
using System;
using System.Linq;

namespace OS_EF_CF_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");

            //User user = new User();
            //user.Username = "User";
            //user.Password = "dXNlcg==";
            //user.Email = "user@xyz.com";
            //user.Role = "user";
            //user.Address = "Balijan Dhekiajuli Sonitpur Assam 784112";
            //user.c = "Balijan Dhekiajuli Sonitpur Assam 784112";


            OnlineShoppingContext dbContext = new OnlineShoppingContext();

            Product product = dbContext.Products.Find(1);

            Cart cart = new Cart();
            cart.Products.Add(product);
            cart.UserId = dbContext.Users.Where(u => u.Email == "demouser@xyz.com").FirstOrDefault().Id;


            dbContext.Carts.Add(cart);
            dbContext.SaveChanges();

            Console.WriteLine("Completed...");
            Console.ReadKey();

        }
    }
}
