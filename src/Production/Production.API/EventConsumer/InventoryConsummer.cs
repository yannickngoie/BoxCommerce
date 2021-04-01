using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using Production.API.Models;
using Production.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.API.EventConsumer
{
    public class InventoryConsummer : IConsumer<ProductionEvent>
    {


        private readonly IMapper _mapper;
        private readonly ILogger<InventoryConsummer> _logger;
        private readonly IProductionRepository _repository;

        public InventoryConsummer(IMapper mapper, ILogger<InventoryConsummer> logger, IProductionRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Consume(ConsumeContext<ProductionEvent> context)
        {
            var message = context.Message;
            var workitem = new Activity { OrderID = message.OrderID, Name = message.Name, OrderStatus = message.OrderStatus, OrderNumber = message.OrderNumber };
            var result = await _repository.AddWorkItem(workitem);


            _logger.LogInformation("ProductionEvent consumed successfully. Work Item Id : {OrderNumber}", result.OrderNumber);
        }

    }
}
