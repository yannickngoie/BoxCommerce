using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string DateCreated { get; set; }
        public string DateModified { get; set; }
        public string ProductType { get; set; }
        public string ProductStatus { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string Price { get; set; }
        public string StockQTY { get; set; }
        public string ProductWeight { get; set; }
        public Category category { get; set; }
        public  Attributes attributes { get; set; }

    }
}
