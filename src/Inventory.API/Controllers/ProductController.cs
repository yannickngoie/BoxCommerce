using Inventory.API.Models;
using Inventory.API.Repositories;
using Inventory.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inventory.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        }

        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _repository.GetProductById(id);
            return Ok(product ?? new Product(id));
        }

        ////[HttpGet("{id}", Name = "P/GetProductInStockById")]
        ////[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<Product>> GetProductInStockById(int id)
        //{
        //    var product = await _repository.GetProductById(id,true);
        //    return Ok(product ?? new Product(id));
        //}


        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProducts()
        {
            var product = await _repository.GetProducts();
            return Ok(product);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repository.UpdateProduct(product));
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(int id)
        {
            await _repository.DeleteProduct(id);
            return Ok();
        }

    }
}
