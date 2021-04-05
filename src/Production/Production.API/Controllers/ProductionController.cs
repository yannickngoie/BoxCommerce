using AutoMapper;
using Common.Logging;
using EventBus.Messages.Events;
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
    [Route("api/[controller]")]
 
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

 

        [HttpPost ( Name = "UpdateWorkItem")]
        [ProducesResponseType(typeof(Activity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Activity>> UpdateWorkItem([FromBody] Activity item)
        {

            return Ok(await _repository.UpdateWorkItem(item));
        }

        [HttpPost("Complete/", Name = "CompleteWorkItem")]
        [ProducesResponseType(typeof(CompleteActivity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Activity>> CompleteWorkItem ([FromBody] CompleteActivity item)
        {
            var result = _mapper.Map<Activity>(item);
            var completeItem = await _repository.CompleteWorkItem(result);
            if (completeItem.OrderStatus == Status.Completed.ToString())
            {
                // Send a notification event for order updateS
                var updateMesssage = new ProductionEvent { OrderID = completeItem.OrderID, OrderStatus = completeItem.OrderStatus };
                await _publishEndpoint.Publish<ProductionEvent>(updateMesssage);
                return Ok($" Order : {completeItem.OrderNumber} has been completed");
            }
               return Ok(HttpStatusCode.NotFound);
        }




        [HttpGet( Name = "GetWorkItems")]
        [ProducesResponseType(typeof(Activity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Activity>> GetWorkItems()
        {
            var result = await _repository.GetWorkItems();
            return Ok(result);
        }
    }
}

