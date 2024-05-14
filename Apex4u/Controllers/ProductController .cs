using Apex4u.DTO;
using Apex4u.Mapper;
using Apex4u.Persistence.Models;
using Apex4u.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apex4u.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{searchEngineFriendlyName}")]
        public async Task<ActionResult<Product>> GetProductBySearchEngineFriendlyName(string searchEngineFriendlyName)
        {
            var product = await _productRepository.GetProductBySearchEngineFriendlyNameAsync(searchEngineFriendlyName);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpGet("{allproduct}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
             [FromQuery] bool? inStock,
             [FromQuery] string variantColor,
             [FromQuery] string variantSize,
             [FromQuery] int? warehouseId,
             [FromQuery] string productName,
             [FromQuery] int page = 1,
             [FromQuery] int pageSize = 10,
             [FromQuery] string sortBy = "CreatedOn",
             [FromQuery] bool sortAscending = false)
        {
            var filter = new ProductFilterDto
            {
                InStock = inStock,
                VariantColor = variantColor,
                VariantSize = variantSize,
                WarehouseId = warehouseId,
                ProductName = productName,
               pageSize = pageSize,
            };

            var pagination = new PaginationDto
            {
                Page = page,
                PageSize = pageSize
            };

            var products = await _productRepository.GetProductsAsync(filter, pagination, sortBy, sortAscending);
            return Ok(products);
        }




    }
}
