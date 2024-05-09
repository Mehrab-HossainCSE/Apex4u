using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apex4u.Persistence.Models
{
    public class Stock
    {
        public int StockID { get; set; } // Primary Key
        public int VariantID { get; set; } // Foreign Key to Variant
        public int WarehouseID { get; set; } // Foreign Key to Warehouse
        public int Quantity { get; set; }

        // Navigation properties
        public Variant Variant { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
