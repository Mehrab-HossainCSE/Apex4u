namespace Apex4u.DTO
{
    public class ProductFilterDto
    {
        public bool? InStock { get; set; }
      
        public string VariantColor { get; set; }
        public string VariantSize { get; set; }
        public string WarehouseName { get; set; }

        public string ProductName { get; set; }
        
    }
}
