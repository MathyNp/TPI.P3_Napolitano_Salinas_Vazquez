using Microsoft.EntityFrameworkCore;
using TPI_NapolitanoSalinasVazquez_P3.Models;

namespace TPI_NapolitanoSalinasVazquez_P3.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Admin> Questions { get; set; }
        public DbSet<Client> Responses { get; set; }
        public DbSet<Product> Subjects { get; set; }
        public DbSet<ShoppingCart> Students { get; set; }
        public DbSet<User> Professors { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 

        }
        
        


    }
}
