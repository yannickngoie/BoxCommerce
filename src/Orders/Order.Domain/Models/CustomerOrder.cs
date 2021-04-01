
using Orders.Domain.Common;
using System.Collections.Generic;

namespace Orders.Domain.Models
{
    /* Order.Domain Layer is created  using the Clean Architecture Pattern*/
    public class CustomerOrder : EntityBase
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public string OrderNumber { get; set; }
        public string IDNumber { get; set; }
        public string ProductID { get; set; }
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

        // Order items
        //public Vehicle vehicle {get;set;}
    }
}

