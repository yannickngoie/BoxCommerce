using MediatR;
using Common.Logging;

namespace Order.Application.Features.Orders.Commands.CheckOutOrder
{
    public class CheckoutOrderCommand : IRequest<ServiceResponse>
    {
      
        // BillingAddress
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string IDNumber { get; set; }
        public string ProductID { get; set; }
        public string ProductCode { get; set; }
        public Vehicle items { get; set; }
    }


    public class CheckoutOrderItemsCommand
    {
        public string ID { get; set; }
        public string OrderID { get; set; }
        public string Name { get; set; }
        public string ProductID { get; set; }
        public string ProductCode { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string SKU { get; set; }
        public string Color { get; set; }

    }
    public class CheckoutOrderComponentCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }


    }

    public class Vehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Component { get; set; }
    }


    


}
