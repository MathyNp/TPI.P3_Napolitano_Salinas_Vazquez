using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TPI_NapolitanoSalinasVazquez_P3.Models;

namespace TPI_NapolitanoSalinasVazquez_P3.Data
{
    public class TPI_NapolitanoSalinasVazquez_P3Context : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator(u => u.UserRol)
                .HasValue<Client>(UserRoleEnum.Client)
                .HasValue<Admin>(UserRoleEnum.Admin);

            modelBuilder.Entity<Admin>()
                .HasBaseType<User>();

            modelBuilder.Entity<Client>()
                .HasBaseType<User>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.History)
                .WithOne()
                .HasForeignKey(h => h.UserId);
                

           

            
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    UserName = "admin",
                    UserPassword = "admin",                  
                    
                    UserMail = "admin@admin.com",
                    UserID = 1

                }
                );

        }

        public TPI_NapolitanoSalinasVazquez_P3Context (DbContextOptions<TPI_NapolitanoSalinasVazquez_P3Context> options)
            : base(options)
        {

        }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.User> Users { get; set; } = default!;

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.Product>? Product { get; set; }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.Client>? Client { get; set; }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.Admin>? Admin { get; set; }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.History>?Histories { get; set; }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.ShoppingCart>? ShoppingCart { get; set; }

      
    }
}
