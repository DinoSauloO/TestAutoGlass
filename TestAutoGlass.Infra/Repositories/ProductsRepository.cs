using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAutoGlass.Domain.Entities;
using TestAutoGlass.Domain.Interfaces.Configuration;
using TestAutoGlass.Domain.Interfaces.Repositories;
using TestAutoGlass.Domain.Requests;
using TestAutoGlass.Infra.Presistence;

namespace TestAutoGlass.Infra.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IPostgreDbContext _dbContext;

        public ProductsRepository(IPostgreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Products>> GetAllAsync(ProductsRequest productParams, int pageNumber, int pageQuantity)
        {
            IQueryable<Products> query = _dbContext.Products.OrderBy(x => x.Id);

            if (productParams.Status != null)
                query = query.Where(x => x.Status == productParams.Status);

            if (productParams.ManufacturingDate > DateTime.MinValue)
                query = query.Where(x => x.ManufacturingDate == productParams.ManufacturingDate);

            if (productParams.ExpirationDate > DateTime.MinValue)
                query = query.Where(x => x.ExpirationDate == productParams.ExpirationDate);

            if (productParams.SupplierId > 0)
                query = query.Where(x => x.SupplierId == productParams.SupplierId);

            if (!string.IsNullOrEmpty(productParams.SupplierCNPJ))
                query = query.Where(x => x.SupplierCNPJ == productParams.SupplierCNPJ);

            if (pageNumber == 0 || pageQuantity == 0)
                return await query.ToListAsync();

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageQuantity);

            if (pageNumber > totalPages)
                pageNumber = totalPages;

            return await query
                .Skip((pageNumber -1) * pageQuantity)
                .Take(pageQuantity)
                .ToListAsync();
        }

        public async Task<Products> CreateAsync(Products product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Products> UpdateAsync(Products product)
        {
            var updatedProd = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            if (updatedProd == null)
                return null;

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return false;

            product.Status = false;
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
