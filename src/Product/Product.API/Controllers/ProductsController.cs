using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Services;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
    

        private static IProductService _service;
        

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Product>>> GetProduct()
        {
            return Ok(await _service.GetAllProducts());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Product>> GetProduct(string id)
        {
            var product = await _service.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, Models.Product product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }
            else
            {
                return Ok(await _service.UpdateProduct(id, product));
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Models.Product>> PostProduct(Models.Product product)
        {
            var result = await _service.CreateProduct(product);
            return CreatedAtAction("GetProduct", new { id = product.ID }, result);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Product>> DeleteProduct(string id)
        {
            var product = await _service.DeleteProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

       
    }
}
