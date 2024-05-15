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

        public async Task<Product> GetProductBySearchNameAsync(string searchEngineFriendlyName)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == searchEngineFriendlyName);
        }
        public async Task<IQueryable<Product>> GetProductsAsync()
        {
            return _dbContext.Products.AsQueryable();
            


        }
       
    }
}
