using AutoMapper;
using EventBus.Messages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Features.Orders.Commands.CheckOutOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.EventConsumer
{

    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMediator mediator, IMapper mapper, ILogger<BasketCheckoutConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var basketCheckoutItems = context.Message.BasketCheckoutItemEvents;
            var basketComponents = context.Message.BasketEventComponents;

            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            ////command.CheckoutOrderItems = new List<CheckoutOrderItemsCommand>();
            ////command.components = new List<CheckoutOrderComponentCommand>();

            ////foreach (var item in basketCheckoutItems)
            ////{
            ////    var orderItemsCommand = _mapper.Map<CheckoutOrderItemsCommand>(item);
            ////    command.CheckoutOrderItems.Add(orderItemsCommand);
            ////}

            ////foreach (var item in basketComponents)
            ////{
            ////    var orderItemsCompCommand = _mapper.Map<CheckoutOrderComponentCommand>(item);
            ////    command.components.Add(orderItemsCompCommand);
            ////}

            var result = await _mediator.Send(command);

            _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result);
        }


    }

}

