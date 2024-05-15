using Apex4u.DTO;

using Apex4u.Persistence.Models;
using Apex4u.Persistence.Repository;
using Apex4u.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apex4u.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productRepository;

        public ProductController(IProductService productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("api/products/{searchEngineFriendlyName}")]
        public async Task<ActionResult<Product>> GetProductBySearchEngineFriendlyName(string searchEngineFriendlyName)
        { 
            var product = await _productRepository.GetProductByName(searchEngineFriendlyName);
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
             [FromQuery] string warehouseName,
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
                WarehouseName = warehouseName,
                ProductName = productName,
              // pageSize = pageSize,
            };

            var pagination = new PaginationDto
            {
                Page = page,
                PageSize = pageSize
            };

            var products = await _productRepository.GetAllProduct(filter, pagination, sortBy, sortAscending);
            return Ok(products);
        }




    }
}
