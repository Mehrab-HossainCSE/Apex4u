namespace Apex4u.Services.DTO
{
    public class ProductDto
    {
        public int ProductID { get; set; } // Primary Key
        public string Name { get; set; }
        public bool? InStock { get; set; }
        public string VariantColor { get; set; }
        public string VariantSize { get; set; }
        public int? WarehouseId { get; set; }
    }
}
