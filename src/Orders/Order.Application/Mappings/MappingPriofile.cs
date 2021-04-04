using AutoMapper;
using EventBus.Messages.Events;
using Order.Application.Features.Orders.Commands.CancelOrder;
using Order.Application.Features.Orders.Commands.CheckOutOrder;
using Order.Application.Features.Orders.Commands.UpdateOrder;
using Order.Application.Features.Orders.Queries.GetOrdersList;
using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Order.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerOrder, OrdersVm>().ReverseMap();
            CreateMap<CustomerOrder, CheckoutOrderCommand>().ReverseMap();
            CreateMap<OrderLines, CheckoutOrderItemsCommand>().ReverseMap();
            CreateMap<StockEvent, CheckoutOrderItemsCommand>().ReverseMap();
            CreateMap<Component, CheckoutOrderComponentCommand>().ReverseMap();
            CreateMap<CustomerOrder, UpdateOrderCommand>().ReverseMap();
            CreateMap<CustomerOrder, CancelOrderCommand>().ReverseMap();
        }
    }
}
