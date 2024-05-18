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
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetProductByName(string name)
        {
            var query = await _repository.GetProductsAsync();
            return await query.Where(p => p.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<List<ProductResponseDTO>> GetAllProduct(ProductFilterDto filter, PaginationDto pagination, string sortBy, bool sortAscending)
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
                query = query.Where(p => p.Variants.Any(v => v.Color.ToLower() == filter.VariantColor.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.VariantSize))
            {
                query = query.Where(p => p.Variants.Any(v => v.Size.ToLower() == filter.VariantSize.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.WarehouseName))
            {
                query = query.Where(p => p.Variants.Any(v => v.Stocks.Any(s => s.Warehouse.Name.ToLower() == filter.WarehouseName.ToLower())));
            }

            if (!string.IsNullOrWhiteSpace(filter.ProductName))
            {
                query = query.Where(p => p.Name.ToLower().Contains(filter.ProductName.ToLower()));
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

            // Map Product entities to ProductResponseDTO objects
            var productDtos = products.Select(product => new ProductResponseDTO
            {
                ProductName = product.Name,
                InStock = product.Variants.Any(v => v.Stocks.Any(s => s.Quantity > 0)),
                Variants = product.Variants.Select(v => new VariantDTO
                {
                    Color = v.Color,
                    Size = v.Size,
                    WarehouseStocks = v.Stocks.Select(s => new WarehouseStockDTO
                    {
                        WarehouseName = s.Warehouse.Name,
                        Quantity = s.Quantity
                    }).ToList()
                }).ToList()
            }).ToList();

            return productDtos;
        }
    }
}
