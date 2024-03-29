﻿using AutoMapper;
using Common.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Exceptions;
using Orders.Application.Contracts.Persistence;
using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands.UpdateOrder
{
   public class UpdateOrderCommandHandler: IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToUpdate == null)
            {
                throw new NotFoundException(nameof(CustomerOrder), request.Id);
            }
            if (request.OrderStatus == Status.Completed.ToString())
            {
                orderToUpdate.OrderStatus = Status.Completed.ToString();
                await _orderRepository.UpdateAsync(orderToUpdate);
            }

            else
            {
                _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(CustomerOrder));
                await _orderRepository.UpdateAsync(orderToUpdate);
            }

                _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}
