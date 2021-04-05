using AutoMapper;
using Common.Logging;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Application.Contracts.Infrastructure;
using Orders.Application.Contracts.Persistence;
using Orders.Application.Models;
using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, ServiceResponse>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CancelOrderCommandHandler> _logger;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IPublishEndpoint publishEndpoint, IEmailService emailService, ILogger<CancelOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task<ServiceResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {

            var service = new ServiceResponse();
            Expression<Func<CustomerOrder, bool>> findOrderNumber = o => o.OrderNumber == request.OrderNumber;

            var itemToUpdate = await _orderRepository.GetAsync(findOrderNumber);

            if (itemToUpdate.Any())
            {
                
                var command = itemToUpdate.FirstOrDefault();
                if (command.OrderStatus ==  Status.Cancelled.ToString())
                {
                
                service.Message = ($"Order {command.Id} is was already cancelled.");

              _logger.LogInformation($"Order {command.Id} is was already cancelled.");

                }
                command.OrderStatus = Status.Cancelled.ToString();
                await _orderRepository.UpdateAsync(command);
                await SendMail(command);

                service.Success = true;
            }
            else
            {
                service.Success = false;
            }

            return service;
        }

        private async Task SendMail(CustomerOrder order)
        {
            var email = new Email() { To = order.EmailAddress, Body = $"Order is cancelled.", Subject = "Order is cancelled" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }

    }
}
