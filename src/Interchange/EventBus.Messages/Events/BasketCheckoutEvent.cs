using System;
using System.Collections.Generic;

namespace EventBus.Messages
{
    public class BasketCheckoutEvent: IntegratonBasedEvent
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

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
        public int PaymentMethod
        {
            get; set;
        }
        public List<BasketCheckoutItemEvent> BasketCheckoutItemEvents { get; set; }
        public List<BasketEventComponent> BasketEventComponents { get; set; }
    }
    public class BasketCheckoutItemEvent
    {
        public string OrderID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string ProductID { get; set; }
        public string Quantity { get; set; }        
        public string Color { get; set; }


    }
    public class BasketEventComponent
    {
        public string Id { get; set; }
        public string Name { get; set; }



    }
}
