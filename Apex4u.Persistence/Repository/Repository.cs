using Apex4u.Persistence.Data;
using Apex4u.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Apex4u.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EFDataContext _dbContext;

        public Repository(EFDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<IEnumerable> GetProductBySearchNameAsync(string searchEngineFriendlyName)
        //{
        //    return _dbContext.Set<T>().AsQueryable();
        //}
        public async Task<IQueryable<T>> GetProductsAsync()
        {
            return _dbContext.Set<T>().AsQueryable();
            


        }
       
    }
}
