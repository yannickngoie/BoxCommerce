using AutoMapper;
using Basket.API.Models;
using EventBus.Messages;
using System.Collections.Generic;

namespace Basket.API.Mapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
            CreateMap<ShoppingCartItem, BasketCheckoutItemEvent>().ReverseMap();
            CreateMap<Component, BasketEventComponent>().ReverseMap();


        }
    }
}
