using Microsoft.EntityFrameworkCore;

namespace Delivery.Model
{
    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options) { }

        public DbSet<Category> Categorys { get; set; }

    }
}
