using Microsoft.EntityFrameworkCore;

namespace Delivery.Model
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Product> Products { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thiết lập điều kiện mặc định cho is_hidden là true
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.is_hidden == true);
        }
    }
}
