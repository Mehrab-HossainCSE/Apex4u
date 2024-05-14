using Apex4u.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Apex4u.Persistence.Data
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Variant> Variants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            
            modelBuilder.Entity<Product>()
                .HasMany(v=>v.Variants).
                WithOne(p=> p.Product).
                HasForeignKey(p=>p.ProductID)
              ;
            modelBuilder.Entity<Variant>()
                .HasMany(v => v.Stocks)
                .WithOne(v => v.Variant)
                .HasForeignKey(s => s.VariantID);
            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.Stocks)
                .WithOne(s => s.Warehouse)
                .HasForeignKey(s => s.WarehouseID);
        }

    }
}
