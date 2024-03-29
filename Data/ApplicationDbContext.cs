using Assignment_3_APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3_APIs.Data
{
    // This class to interact with the database
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define the tables in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure decimal properties to avoid truncation warnings
            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.ShippingCost)
                .HasColumnType("decimal(18, 2)");

            // Add any other configurations here
        }
    }
}
