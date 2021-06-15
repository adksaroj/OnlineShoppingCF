using OnlineShoppingDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_EF_CF_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");

            User user = new User();
            user.Username = "User";
            user.Password = "dXNlcg==";
            user.Email = "user@xyz.com";
            user.Role = "user";
            user.Address = "Balijan Dhekiajuli Sonitpur Assam 784112";

            OnlineShoppingContext dbContext = new OnlineShoppingContext();

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            Console.WriteLine("Completed...");
            Console.ReadKey();

        }
    }
}
