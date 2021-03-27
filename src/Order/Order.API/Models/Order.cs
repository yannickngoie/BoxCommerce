using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public Guid OrderId { get; set; }

        [BsonElement("OrderNumber")]
        public string OrderNumber { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public string DateCreated { get; set; }
        public string DateModified { get; set; }
        public string CustomerID { get; set; }
        public string CustomerNote { get; set; }
        public OrderLines LineItems { get; set; }
        public BillingDetails Billing {get;set; }
        public ShippingDetails Shipping { get; set; }
        public string TransactionID { get; set; }
        public string DateCompleted { get; set; }
        public bool IsPaid { get; set; }
  
    }
}
