using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apex4u.Persistence.Models
{
    public class Warehouse
    {
        public int WarehouseID { get; set; } // Primary Key
        public string Name { get; set; }
        public string Location { get; set; }

        // Navigation property for stocks
        public ICollection<Stock> Stocks { get; set; }
    }
}
