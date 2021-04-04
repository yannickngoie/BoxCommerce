using Inventory.API.Controllers;
using Inventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Repositories.Interfaces
{
     public interface IProductTypeRepository
    {
        Task<ProductType> GetProductTypeById(int id);
        Task<IEnumerable<ProductType>> GetProductTypes();
        Task<ProductType> InsertProductType(ProductType product);
        Task<ProductType> UpdateProductType(ProductType basket);
        Task DeleteProductType(int id);
    }
}
