using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Entities
{
    public class ShippingDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string AddressLine_1 { get; set; }
        public string AddressLine_2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string  Country { get; set; }
        public string PostalCode { get; set; }

    }
}
