using AutoMapper;
using EventBus.Messages.Events;
using Inventory.API.Repositories.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Inventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;

namespace Inventory.API.EventConsumer
{
    public class OrderConsumer : IConsumer<StockEvent>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderConsumer(IProductRepository repository, IMapper mapper, ILogger<OrderConsumer> logger, IPublishEndpoint publishEndpoint)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));

        }

        public async Task Consume(ConsumeContext<StockEvent> context)
        {
            int result;
            var stockMessage = context.Message;
            var success = Int32.TryParse(context.Message.ProductID, out result);
            var product = new Product();
            product = await _repository.GetProductById(result);

            if (product != null)
            {
                // if product in stock update order status to INPROGESS
                if (product.InStock)
                {
                    int orderId;
                    var output = Int32.TryParse(context.Message.OrderID, out orderId);

                    var eventMessage = _mapper.Map<InventoryUpdateEvent>(stockMessage);
                    eventMessage.OrderStatus = Status.InProgress.ToString();
                    await _publishEndpoint.Publish<InventoryUpdateEvent>(eventMessage);

                }
            }

            else
            {
                //SCHEDULE_PRODUCTION

                var eventMessage =  new ProductionEvent
                    {
                        OrderID = stockMessage.OrderID,
                        ProductID = stockMessage.ProductID,
                        OrderNumber = stockMessage.OrderNumber,
                        OrderStatus = Status.InProduction.ToString(),
                        ProductName = stockMessage.Name
                    };

                    await _publishEndpoint.Publish<ProductionEvent>(eventMessage);
                }

                _logger.LogInformation("StockEvent consumed successfully. Created Order Number : {newOrder}", context.Message.OrderNumber);
            }

        }
    }
