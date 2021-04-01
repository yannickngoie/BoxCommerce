using Inventory.API.Data;
using Inventory.API.Models;
using Inventory.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly InventoryContext _dbContext;
        public ProductTypeRepository(InventoryContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task DeleteProductType(int id)
        {
            var product = new ProductType(id);
            _dbContext.Entry(product).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProductType> GetProductTypeById(int id)
        {
            return await _dbContext.ProductTypes.FindAsync(id);
        }

        public async Task<IEnumerable<ProductType>> GetProductTypes()
        {
            return await _dbContext.ProductTypes.ToListAsync();
        }

        public async Task<ProductType> InsertProductType(ProductType productType)
        {
            _dbContext.ProductTypes.Add(productType);
            await _dbContext.SaveChangesAsync();
            var result = productType;

            return result;
        }

        public async Task<ProductType> UpdateProductType(ProductType productType)
        {
            _dbContext.ProductTypes.Update(productType);
            await _dbContext.SaveChangesAsync();
            var result = productType;

            return result;
        }
    }
}
