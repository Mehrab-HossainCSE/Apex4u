namespace Apex4u.DTO
{
    public class ProductFilterDto
    {
        public bool? InStock { get; set; }
        public string VariantColor { get; set; }
        public string VariantSize { get; set; }
        public int? WarehouseId { get; set; }
        public string ProductName { get; set; }
        public  int PageSize { get; set; }
        public int Page {  get; set; }
    }
}
