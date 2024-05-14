using Apex4u.DTO;
using Apex4u.DTODTOInput;
using Apex4u.Persistence.Models;
using Apex4u.Persistence.Repository;
using Apex4u.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace Apex4u.Services.Services
{
    public class ProductService
    {
        string productKey = "productBasekey.";
        private readonly IRepository<Product> _repository;
        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Product> GetProductById(int id)
        {
            string key = $"productBasekey.getById.{id}";

            // var product = await _repository.GetProductById(id);

            Product product = new Product();

            return product;
        }
        public async Task<List<ProductDto>> GetAllProduct(ProductFilterDto request)
        {
            var query = await _repository.GetProductsAsync();

            query = query.Include(p => p.Variants)
                        .ThenInclude(v => v.Stocks)
                        .ThenInclude(s => s.Warehouse);

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                query = query.Where(p => p.Name.Contains(request.ProductName));
            }

            if (request.InStock.HasValue)
            {
                query = query.Where(p => p.Variants.Any(v => v.Stocks.Any(s => s.Quantity > 0)) == request.InStock.Value);
            }

            // Apply other filtering conditions as needed

            // Apply sorting
            query = query.OrderBy(p => p.CreatedOn); // Example: Sort by CreatedOn in ascending order

            var products = await query.Skip(request.PageSize * (request.Page - 1))
                                      .Take(request.PageSize)
                                      .ToListAsync();

            return products.Adapt<List<ProductDto>>(); // Assuming you're using AutoMapper for object mapping
        }
    }
}
