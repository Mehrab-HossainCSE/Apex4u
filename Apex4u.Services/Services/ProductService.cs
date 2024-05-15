using Apex4u.DTO;
using Apex4u.Persistence.Models;
using Apex4u.Persistence.Repository;
using Apex4u.Services.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apex4u.Services.Services
{
    public class ProductService: IProductService
    {
        string productKey = "productBasekey.";
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetProductByName(string name)
        {

          var product= await _repository.GetProductBySearchNameAsync(name);
            return product;
        }

        public async Task<List<ProductDto>> GetAllProduct(ProductFilterDto filter, PaginationDto pagination, string sortBy, bool sortAscending)
        {
            // Get IQueryable<Product> from your repository
            var query = await _repository.GetProductsAsync();

            // Include related entities if needed
            query = query.Include(p => p.Variants)
                         .ThenInclude(v => v.Stocks)
                         .ThenInclude(s => s.Warehouse);

            // Apply filters based on filter object
            if (filter.InStock.HasValue)
            {
                if (filter.InStock == true)
                {
                    query = query.Where(p => p.Variants.Any(v => v.Stocks.Any(s => s.Quantity > 0)));
                }
                else
                {
                    query = query.Where(p => !p.Variants.Any(v => v.Stocks.Any(s => s.Quantity > 0)));
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.VariantColor))
            {
                query = query.Where(p => p.Variants.Any(v => v.Color == filter.VariantColor));
            }

            if (!string.IsNullOrWhiteSpace(filter.VariantSize))
            {
                query = query.Where(p => p.Variants.Any(v => v.Size == filter.VariantSize));
            }

            if (!string.IsNullOrWhiteSpace(filter.WarehouseName))
            {
                query = query.Where(p => p.Variants.Any(v => v.Stocks.Any(s => s.Warehouse.Name == filter.WarehouseName)));
            }


            if (!string.IsNullOrWhiteSpace(filter.ProductName))
            {
                query = query.Where(p => p.Name.Contains(filter.ProductName));
            }

            // Apply sorting
            switch (sortBy)
            {
                case "Name":
                    query = sortAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                    break;
                case "CumulativeStock":
                    // Calculate cumulative stock and sort by it
                    query = sortAscending ? query.OrderBy(p => p.Variants.Sum(v => v.Stocks.Sum(s => s.Quantity))) :
                                             query.OrderByDescending(p => p.Variants.Sum(v => v.Stocks.Sum(s => s.Quantity)));
                    break;
                default:
                    query = query.OrderBy(p => p.CreatedOn);
                    break;
            }

            // Apply pagination
            var products = await query.Skip(pagination.PageSize * (pagination.Page - 1))
                                      .Take(pagination.PageSize)
                                      .ToListAsync();

            // Map Product entities to ProductDto objects
            var productDtos = products.Select(product => new ProductDto
            {
                // Map properties manually
                ProductID = product.ProductID,
                Name = product.Name,
                InStock = product.Variants.Any(v => v.Stocks.Any(s => s.Quantity > 0)),
                VariantColor = product.Variants.Select(v => v.Color).FirstOrDefault(),
                VariantSize = product.Variants.Select(v => v.Size).FirstOrDefault(),
               
            }).ToList();

            return productDtos;
        }

    }
}
