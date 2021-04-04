using Inventory.API.Repositories.Interfaces;
using Inventory.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        // GET: api/<ComponentController>
        private readonly IComponentRepository _repository;

        public ComponentController(IComponentRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        }

        [HttpGet("{id}", Name = "GetComponentById")]
        [ProducesResponseType(typeof(Component), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Component>> GetComponentById(Guid id)
        {
            var result = await _repository.GetComponentById(id);

            return Ok(result);
        }


        [HttpGet(Name = "GetComponents")]
        [ProducesResponseType(typeof(Component), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Component>> GetComponents()
        {
            var product = await _repository.GetComponents();
            return Ok(product);
        }


        [HttpPut]
        [ProducesResponseType(typeof(Component), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Component>> UpdateComponent([FromBody] Component component)
        {
            return Ok(await _repository.UpdateComponent(component));
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Component>> InsertComponent([FromBody] Component component)
        {
            var result = await _repository.InsertComponent(component);
            return Ok($"New record  ID: " +  result.ID.ToString());
        }

        [HttpDelete("{id}", Name = "DeleteComponent")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteComponent(Guid id)
        {
            await _repository.DeleteComponent(id);
            return Ok();
        }
    }
}
