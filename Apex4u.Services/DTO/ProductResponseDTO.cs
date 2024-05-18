using System.Collections.Generic;

namespace Apex4u.Services.DTO
{
    public class WarehouseStockDTO
    {
        public string WarehouseName { get; set; }
        public int Quantity { get; set; }
    }

    public class VariantDTO
    {
        public string Color { get; set; }
        public string Size { get; set; }
        public List<WarehouseStockDTO> WarehouseStocks { get; set; }
    }

    public class ProductResponseDTO
    {
        public string ProductName { get; set; }
        public bool InStock { get; set; }
        public List<VariantDTO> Variants { get; set; }
    }
}
