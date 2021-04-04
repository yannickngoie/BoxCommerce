using Inventory.API.Models;
using Inventory.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Inventory.API.Controllers
{
    [Route("api/ProductType")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _repository;

        public ProductTypeController(IProductTypeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        }

        [HttpGet("{id}", Name = "GetProductTypeById")]
        [ProducesResponseType(typeof(ProductType), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductType>> GetProductTypeById(int id)
        {
            var product = await _repository.GetProductTypeById(id);
            return Ok(product ?? new ProductType(id));
        }
        [HttpGet(Name = "GetProductTypes")]
        [ProducesResponseType(typeof(ProductType), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            var product = await _repository.GetProductTypes();
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductType), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductType>> UpdateProductType([FromBody] ProductType productType)
        {
            return Ok(await _repository.UpdateProductType(productType));
        }

        [HttpDelete("{id}", Name = "DeleteProductType")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductType(int id)
        {
            await _repository.DeleteProductType(id);
            return Ok();
        }
    }
}
