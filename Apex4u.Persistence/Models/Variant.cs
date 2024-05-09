using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apex4u.Persistence.Models
{
    public class Variant
    {
        public int VariantID { get; set; } // Primary Key
        public int ProductID { get; set; } // Foreign Key to Product
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
     

        // Navigation properties
        public Product Product { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
