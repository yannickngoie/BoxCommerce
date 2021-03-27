using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.API.Models;

namespace Product.API.Services
{
   public interface IProductService
    {
        Task<ServiceResponse> CreateProduct(Models.Product product);
        Task<Models.Product> UpdateProduct(string id, Models.Product product);
        Task<ServiceResponse> DeleteProduct (string id );
        Task <IEnumerable<Models.Product>> GetAllProducts();
        Task <Models.Product> GetProduct(string id);
        Task<ServiceResponse> BatchUpdate (Models.Product product);


    }
}
