using Apex4u.DTO;

namespace Apex4u.Mapper
{
    public class ProductMappercs
    {
        private ProductFilterDto ConvertToProductFilter(ProductFilterDto filterDto)
        {
            // Implement conversion logic from ProductFilterDto to ProductFilter
            // Example:
            var productFilter = new ProductFilterDto
            {
                InStock = filterDto.InStock,
                VariantColor = filterDto.VariantColor,
                VariantSize = filterDto.VariantSize,
                WarehouseId = filterDto.WarehouseId,
                ProductName = filterDto.ProductName
            };
            return productFilter;
        }
    }
}
