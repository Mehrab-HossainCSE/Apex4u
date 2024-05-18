using Apex4u.DTO;
using Apex4u.Persistence.Models;
using Apex4u.Services.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apex4u.Services.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByName(string name);
        Task<List<ProductResponseDTO>> GetAllProduct(ProductFilterDto filter,PaginationDto pagination,string sortBy,bool sortAscending);
    }
}
