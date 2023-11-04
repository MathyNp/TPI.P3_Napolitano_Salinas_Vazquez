using Microsoft.EntityFrameworkCore;
using TPI_NapolitanoSalinasVazquez_P3.Models;

namespace TPI_NapolitanoSalinasVazquez_P3
{
    public class MarketContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Product> Products { get; set; }

        public MarketContext(DbContextOptions<MarketContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
