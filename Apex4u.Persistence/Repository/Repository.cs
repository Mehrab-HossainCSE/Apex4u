using Apex4u.Persistence.Data;
using Apex4u.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Apex4u.Persistence.Repository
{
    public class Repository<T> : IRepository<Product>
    {
        private readonly EFDataContext _dbContext;

        public Repository(EFDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> GetProductBySearchEngineFriendlyNameAsync(string searchEngineFriendlyName)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == searchEngineFriendlyName);
        }
        public async Task<IQueryable<Product>> GetProductsAsync()
        {
            return _dbContext.Products.AsQueryable();
            /*IQueryable
             IEnumerable;
            ICollection
                IList*/


        }
        /*public async Task<Queryable<Product>> GetProductsAsync(ProductFilterDto filter, PaginationDto paginationstring,string sortAscending, bool sortBy)
        {
            return _dbContext.Products.AsQueryable()
            *//*var query = _dbContext.Products
                .Include(p => p.Variants)
                .ThenInclude(v => v.Stocks)
                .ThenInclude(s => s.Warehouse)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(filter.SearchTerm));
            }

            if (filter.InStock.HasValue)
            {
                query = query.Where(p => p.Variants.Any(v => v.Stocks.Any(s => s.Quantity > 0)));
            }

            // Apply pagination
            query = query.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize);

            return await query.ToListAsync();*//*
        }*/
    }
}
