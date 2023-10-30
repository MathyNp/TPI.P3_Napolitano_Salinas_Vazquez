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
        public TPI_NapolitanoSalinasVazquez_P3Context (DbContextOptions<TPI_NapolitanoSalinasVazquez_P3Context> options)
            : base(options)
        {
        }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.User> User { get; set; } = default!;

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.Product>? Product { get; set; }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.Client>? Client { get; set; }

        public DbSet<TPI_NapolitanoSalinasVazquez_P3.Models.Admin>? Admin { get; set; }
    }
}
