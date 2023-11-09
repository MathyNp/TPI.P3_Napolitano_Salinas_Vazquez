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

            modelBuilder.Entity<SaleOrderLine>()
                .HasOne(sol => sol.Product)
                .WithMany()
                .HasForeignKey(sol => sol.ProductId);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(sc => sc.saleOrderLines)
                .WithOne()
                .HasForeignKey(sol => sol.SaleOrderLineId);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.UserCart)
                .WithOne()
                .HasForeignKey<Client>(c => c.UserCartId);

        }

        public TPI_NapolitanoSalinasVazquez_P3Context (DbContextOptions<TPI_NapolitanoSalinasVazquez_P3Context> options)
            : base(options)
        {

        }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.User> Users { get; set; } = default!;

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.Product>? Product { get; set; }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.Client>? Client { get; set; }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.Admin>? Admin { get; set; }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.SaleOrderLine>? SaleOrderLine { get; set; }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.ShoppingCart>? ShoppingCart { get; set; }

      
    }
}
