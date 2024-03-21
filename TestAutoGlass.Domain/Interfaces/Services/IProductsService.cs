using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutoGlass.Domain.Entities;
using TestAutoGlass.Domain.Requests;

namespace TestAutoGlass.Domain.Interfaces.Services
{
    public interface IProductsService
    {
        Task<Products> GetByIdAsync(int id);
        Task<IEnumerable<Products>> GetAllAsync(ProductsRequest productParams, int pageNumber, int pageQuantity);
        Task<Products> CreateAsync(Products product);
        Task<Products> UpdateAsync(Products product);
        Task<bool> DeleteAsync(int id);
    }
}
