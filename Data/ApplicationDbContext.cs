using Assignment_3_APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3_APIs.Data
{
    // interact with the database
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // tables in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Comment> comment { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


    }
}
