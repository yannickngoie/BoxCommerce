using Inventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int productId, bool inStock = false);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> InsertProduct(Product product);
        Task<Product> UpdateProduct(Product basket);
        Task DeleteProduct(int id);
        Task<Product> GetProductStock(string productId, string orderNumber);
    }
}
