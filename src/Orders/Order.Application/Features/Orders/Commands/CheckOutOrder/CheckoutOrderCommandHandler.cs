using AutoMapper;
using MassTransit;
using MediatR;
using EventBus.Messages;
using Microsoft.Extensions.Logging;
using Orders.Application.Contracts.Infrastructure;
using Orders.Application.Contracts.Persistence;
using Orders.Application.Models;
using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventBus.Messages.Events;
using Common.Logging;

namespace Order.Application.Features.Orders.Commands.CheckOutOrder
{
    class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, ServiceResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IPublishEndpoint publishEndpoint, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task<ServiceResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {

            var orderDetails = request.items;
            CustomerOrder newOrder = new CustomerOrder();
            var service = new ServiceResponse();

            if (request != null)
            {
                var orderEntity = _mapper.Map<CustomerOrder>(request);
                orderEntity.OrderNumber = GenerateOrderNumber();
                orderEntity.OrderStatus = Status.Created.ToString();
                newOrder = await _orderRepository.AddAsync(orderEntity);
                var newOrderLine = new OrderLines();

                if (orderDetails != null)
                {

                    newOrderLine = await _orderRepository.AddAsyncOrderLine(new OrderLines
                    {
                        OrderID = newOrder.Id.ToString(),
                        Name = orderDetails.Make,
                        ProductID = orderEntity.ProductID
                    });

                }
                service.Message = orderEntity.OrderNumber;

                var stockMessage = new StockEvent
                {
                    OrderID = newOrder.Id.ToString(),
                    OrderNumber = orderEntity.OrderNumber,
                    ProductID = newOrderLine.ProductID,
                    OrderStatus = orderEntity.OrderStatus,
                    EmailAddress = orderEntity.EmailAddress,
                    LastName = orderEntity.LastName,
                    FirstName = orderEntity.FirstName,
                    IDNumber = orderEntity.IDNumber,
                };

                await _publishEndpoint.Publish<StockEvent>(stockMessage);

            }

            _logger.LogInformation($"Order {newOrder.Id} is successfully created.");
            _logger.LogInformation($"Inventory check for {newOrder.Id} is successfully sent.");

            await SendMail(newOrder);

            return service;

        }

        private async Task SendMail(CustomerOrder order)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }


        public static string GenerateOrderNumber()
        {

            var random = new Random();
            int length = 16;
            var OrderNumber = "";
            for (var i = 0; i < length; i++)
            {
                OrderNumber += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
            }

            return OrderNumber;
        }
    }
}
