using AutoMapper;
using EventBus.Messages;
using EventBus.Messages.Events;
using Inventory.API.Models;

namespace Inventory.API.Mapper
{
    public class InventoryProfiling: Profile
    {
        public InventoryProfiling()
        {
            CreateMap<InventoryUpdateEvent, StockEvent>().ReverseMap();
            CreateMap<ProductionEvent, StockEvent>().ReverseMap();
        }

    }
  
}
