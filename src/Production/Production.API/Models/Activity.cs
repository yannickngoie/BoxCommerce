using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.API.Models
{
    public class Activity
    {
        public Guid ID { get; set; }
        public string OrderID { get; set; }
        public string OrderStatus { get; set; }
        public string OrderNumber { get; set; }
        public string  Name { get; set; }
        public string Type { get; set; }

    }
}
