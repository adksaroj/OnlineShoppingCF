using OnlineShoppingDataAccess;
using System.Linq;

namespace OnlineShopping.Utilities
{
    public class UserUtility
    {
        public static User GetUserByUserId(string emailId)
        {
            using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
            {
                var user = dbContext.Users.Where(u => u.Email == emailId).FirstOrDefault();
                return user;
            }
        }
    }
}