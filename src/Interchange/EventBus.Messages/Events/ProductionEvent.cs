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
        public string ProductName { get; set; }
        public string ProductID { get; set; }
        public string ProducType { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }
    }
}
