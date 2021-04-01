using AutoMapper;
using EventBus.Messages.Events;
using Inventory.API.Repositories.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Inventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.EventConsumer
{
    public class OrderConsumer: IConsumer<ProductStockAvailabilityEvent>
    {
        private readonly IProductRepository _repository;
            private readonly IMapper _mapper;
            private readonly ILogger<OrderConsumer> _logger;

            public OrderConsumer(IProductRepository repository,IMapper mapper, ILogger<OrderConsumer> logger)
            {
        
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

            public async Task Consume(ConsumeContext<ProductStockAvailabilityEvent> context)
            {
                var command = _mapper.Map<Product>(context.Message);
            var result = await _repository.CheckAvailableProductStock(command);

            await context.RespondAsync<ProductStockStatusResult>(new
            {
                result.ProductId,
                result.ProductName,
                result.InStock
            });

            _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result);
        }

    }
}
