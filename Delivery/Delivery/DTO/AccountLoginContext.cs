using Microsoft.EntityFrameworkCore;

namespace Delivery.DTO
{
    public class AccountLoginContext : DbContext
    {
        public AccountLoginContext(DbContextOptions<AccountLoginContext> options) : base(options) { }
        public DbSet<AccountLogin> accountLogins { get; set; }
    }
}
