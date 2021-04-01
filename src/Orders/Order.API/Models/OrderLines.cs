using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Entities
{
    public class OrderLines
    {
        public string ID { get; set; }
        public string OrderID { get; set; }
        public string Name { get; set; }
        public string ProductID { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string  SKU { get; set; }

    }
}
