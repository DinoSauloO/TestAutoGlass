using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutoGlass.Domain.Entities;
using TestAutoGlass.Domain.Interfaces.Repositories;
using TestAutoGlass.Domain.Interfaces.Services;
using TestAutoGlass.Domain.Requests;

namespace TestAutoGlass.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<Products> GetByIdAsync(int id) =>
            await _productsRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Products>> GetAllAsync(ProductsRequest productParams, int pageNumber, int pageQuantity) =>
            await _productsRepository.GetAllAsync(productParams, pageNumber, pageQuantity);

        public async Task<Products> CreateAsync(Products product) =>
            await _productsRepository.CreateAsync(product);

        public async Task<Products> UpdateAsync(Products product) =>
            await _productsRepository.UpdateAsync(product);

        public async Task<bool> DeleteAsync(int id) =>
            await _productsRepository.SoftDeleteAsync(id);
    }
}
