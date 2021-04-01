using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Production.API.Models;
using Production.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Production.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
 
    public class ProductionController : ControllerBase
    {
        private readonly IProductionRepository _repository;
       
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public ProductionController(IProductionRepository repository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

 

        [HttpPost]
        [ProducesResponseType(typeof(Activity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Activity>> UpdateWorkItem([FromBody] Activity item)
        {

            return Ok(await _repository.UpdateWorkItem(item));
        }
    }
}

