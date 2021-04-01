using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.EventConsumer
{
    public class ProductStockStatusResult
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public bool InStock { get; set; }
    }
}
