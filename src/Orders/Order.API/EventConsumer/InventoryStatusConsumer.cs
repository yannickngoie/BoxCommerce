using AutoMapper;
using EventBus.Messages;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Features.Orders.Commands.UpdateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.EventConsumer
{
    public class InventoryStatusConsumer : IConsumer<InventoryUpdateEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<InventoryStatusConsumer> _logger;

        public InventoryStatusConsumer(IMediator mediator, IMapper mapper, ILogger<InventoryStatusConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task Consume(ConsumeContext<InventoryUpdateEvent> context)
        {
            var eventMessage = context.Message;
            int orderId;
            var success = Int32.TryParse(eventMessage.OrderID, out orderId);
            var command = _mapper.Map<UpdateOrderCommand>(eventMessage);
            command.Id = orderId;
            command.OrderStatus = eventMessage.OrderStatus;
            

            await _mediator.Send(command);
        }
            
    }
}
