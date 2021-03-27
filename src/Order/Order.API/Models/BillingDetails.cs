using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Entities
{
    public class BillingDetails: ShippingDetails
    {
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
