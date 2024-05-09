using Apex4u.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apex4u.Persistence.Data
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options) { }
        DbSet<Product> Products { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<Warehouse> Warehouses { get; set; }
        DbSet<Variant> Variants { get; set; }
        
        public override 

    }
}
