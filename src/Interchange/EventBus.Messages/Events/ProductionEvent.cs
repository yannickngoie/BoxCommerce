using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Messages.Events
{
    public class ProductionEvent: IntegratonBasedEvent
    {
        public string OrderID { get; set; }
        public string OrderStatus { get; set; }
        public string OrderNumber { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
