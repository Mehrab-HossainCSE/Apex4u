using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apex4u.Persistence.Models
{
    public class Product
    {
        public int ProductID { get; set; } // Primary Key
        public string Name { get; set; } // Search engine friendly name
        public string Description { get; set; } // Optional
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; } // Optional

        // Navigation property for variants
        public ICollection<Variant> Variants { get; set; }
    }
}
