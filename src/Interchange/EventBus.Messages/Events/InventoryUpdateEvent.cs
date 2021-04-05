using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Messages.Events
{
    public class InventoryUpdateEvent: IntegratonBasedEvent
    {
        public string OrderID { get; set; }
        public string OrderNumber { get; set; }
        public string Name { get; set; }
        public string ProductID { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string IDNumber { get; set; }
        public string ProductCode { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SKU { get; set; }
        public string OrderStatus { get; set; }
    }
}
