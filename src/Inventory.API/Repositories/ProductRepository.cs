using Inventory.API.Data;
using Inventory.API.Models;
using Inventory.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inventory.API.Repositories
{
    public class ProductRepository: IProductRepository
    {
        protected readonly InventoryContext _dbContext;
        public ProductRepository(InventoryContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task DeleteProduct(int id)
        {
            var product = new Product(id);
            _dbContext.Entry(product).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();

        }
        public async Task<Product> GetProductById(int id, bool inStock)
        {
            var product = new Product();

            if (inStock)
            {
                product = await _dbContext.Products.Where(x => x.ProductId == id && x.InStock == inStock).FirstOrDefaultAsync();
            }
            else
            {
                product = await _dbContext.Products.FindAsync(id);
            }

            return product;
        }
  
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();

        }
        public async Task<Product> InsertProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            var result = product;

            return result;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            var result = product;

            return result;
        }

        public async Task <Product> CheckAvailableProductStock(Product product)
        {
            var result = await GetProductById(product.ProductId, true);
            return result;
            
        }
    }
}
