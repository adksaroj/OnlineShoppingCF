namespace OnlineShoppingDataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineShoppingDataAccess.OnlineShoppingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OnlineShoppingDataAccess.OnlineShoppingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            User user = new User();
            user.Username = "DemoUser";
            user.Password = "ZGVtb3VzZXI=";
            user.Email = "demouser@xyz.com";
            user.Role = "user";
            user.Address = "Dhekiajuli Sonitpur Assam 784112";

            context.Users.AddOrUpdate(user);
        }
    }
}
