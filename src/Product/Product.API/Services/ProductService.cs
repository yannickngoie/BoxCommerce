using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Services
{
    public class ProductService : IProductService
    {

        private readonly ProductContext _context;
        private readonly ServiceResponse serviceResponse = new ServiceResponse();

        public ProductService (ProductContext context)
       {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
       }

        public async Task <ServiceResponse> BatchUpdate(Models.Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> CreateProduct(Models.Product product)
        {
         
            try
            {             
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                serviceResponse.Success = true;
                serviceResponse.Message = "Product created successfully!";

            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ID))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "This product already exists!";

                    return  serviceResponse;
                }
                else
                {
                    throw;
                }
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Record Not Found";

                return serviceResponse;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            serviceResponse.Success = true;
            serviceResponse.Message = "Record deleted successfully";

            return serviceResponse;
        }

        public async Task<IEnumerable<Models.Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Models.Product> UpdateProduct(string id, Models.Product product)
        {
           
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {

                    serviceResponse.Success = false;
                    serviceResponse.Message = "Record Not Found";
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return product;

        }

       public async Task<Models.Product> GetProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
              
            }
            return product;
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
