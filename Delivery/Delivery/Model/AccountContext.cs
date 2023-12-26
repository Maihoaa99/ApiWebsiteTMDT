using Delivery.DTO;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Model
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

        public DbSet<Accounts> Accounts { get; set; }
    }
}
