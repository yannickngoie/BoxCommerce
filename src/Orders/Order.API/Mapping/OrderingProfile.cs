using AutoMapper;
using EventBus.Messages;
using EventBus.Messages.Events;
using Order.Application.Features.Orders.Commands.CheckOutOrder;
using Order.Application.Features.Orders.Commands.UpdateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Mapping
{
    public class OrderingProfile: Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
            CreateMap<CheckoutOrderItemsCommand, BasketCheckoutItemEvent>().ReverseMap();
            CreateMap<BasketEventComponent, CheckoutOrderComponentCommand>().ReverseMap();
            CreateMap<CheckoutOrderCommand, ProductionEvent>().ReverseMap();
            CreateMap<UpdateOrderCommand, InventoryUpdateEvent>().ReverseMap();
        }
    }
}
