using OnlineShoppingDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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