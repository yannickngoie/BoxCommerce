using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Models
{
    public class Product
    {
   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        [JsonIgnore]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }

        [JsonIgnore]
        public int ProductCode { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Barcode { get; set; }
        public string ProductImageUrl { get; set; }
        [Display(Name = "UOM")]
        public int UnitOfMeasureId { get; set; }
        public double DefaultBuyingPrice { get; set; } = 0.0;
        public double DefaultSellingPrice { get; set; } = 0.0;

        private bool instock = true;
        
        [JsonIgnore]
        public bool InStock
        {
            get
            {
                return instock;
            }

            set
            {
                instock = value;
            }
        }
        
        

        public Product()
        {
            // this.ProductId = productId;
        }
        public Product(int productId)
        {
            this.ProductId = productId;
        }

       
    }
}
