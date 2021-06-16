using System.Data.Entity;

namespace OnlineShoppingDataAccess
{
    public partial class OnlineShoppingContext : DbContext
    {
        public OnlineShoppingContext()
            : base("name=OnlineShoppingDB")
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Carts)
                .Map(m => m.ToTable("CartProducts").MapLeftKey("CartId").MapRightKey("ProductId"));

            modelBuilder.Entity<Order>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Orders)
                .Map(m => m.ToTable("OrderItems").MapLeftKey("OrderId").MapRightKey("ProductId"));

            modelBuilder.Entity<Product>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ClientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
