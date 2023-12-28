using Microsoft.EntityFrameworkCore;

namespace Delivery.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thiết lập điều kiện mặc định cho is_hidden là true
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.is_hidden == true);
        }
    }
}
