using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Messages.Events
{
   public class StockEvent: IntegratonBasedEvent
   {
        public string OrderID { get; set; }
        public string OrderNumber { get; set; }
        public string Name { get; set; }
        public string  ProductID { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string SKU { get; set; }

    };
}
