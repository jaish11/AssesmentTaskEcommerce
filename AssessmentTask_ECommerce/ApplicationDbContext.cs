using Microsoft.EntityFrameworkCore;
using AssignmentTaskECommerce.Models;
using E_Commerce.Models;

namespace AssignmentTaskECommerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.ExecuteSqlRaw("DROP TABLE IF EXISTS Sales");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }
    }
}
