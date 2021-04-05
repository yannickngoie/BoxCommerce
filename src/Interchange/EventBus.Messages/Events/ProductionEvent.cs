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
        public string IDNumber { get; set; }
        public string ProductCode { get; set; }
        // BillingAddress
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}
