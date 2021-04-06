using AutoMapper;
using EventBus.Messages.Events;
using Common.Logging;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using Production.API.Models;
using Production.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit.Transports;

namespace Production.API.EventConsumer
{
    public class InventoryConsummer : IConsumer<ProductionEvent>
    {


        private readonly IMapper _mapper;
        private readonly ILogger<InventoryConsummer> _logger;
        private readonly IProductionRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;


        public InventoryConsummer(IMapper mapper, ILogger<InventoryConsummer> logger, IProductionRepository repository, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task Consume(ConsumeContext<ProductionEvent> context)
        {
            var message = context.Message;
            message.OrderStatus = Status.InProduction.ToString();
            var workitem = new Activity {

                OrderID = message.OrderID,
                ProductName = message.ProductName,
                ProductID = message.ProductID,
                OrderStatus = message.OrderStatus, 
                OrderNumber = message.OrderNumber,
                Description = message.Description,
                Component = message.Component
            };

            var result = await _repository.AddWorkItem(workitem);
            

           

            await _publishEndpoint.Publish<ProductionEvent>(message);

            _logger.LogInformation("ProductionEvent consumed successfully. Work Item Id : {OrderNumber}", result.OrderNumber);

           
            // need to update customer and order status like email etc 
            _logger.LogInformation("Order Update for  Work Item OrderNumber : {OrderNumber} ", workitem.OrderNumber + " with Order status as "+ workitem.OrderStatus + " sent successfully  with correlationId "+  context.Message.CorrelationId);
        }

    }
}
