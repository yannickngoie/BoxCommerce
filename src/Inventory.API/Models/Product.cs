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
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public string ProductImageUrl { get; set; }
        [Display(Name = "UOM")]
        public int UnitOfMeasureId { get; set; }
        public double DefaultBuyingPrice { get; set; } = 0.0;
        public double DefaultSellingPrice { get; set; } = 0.0;
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        private bool instock; 
        public bool InStock
        {
            get
            {
                return instock;
            }

            set
            {
                instock = true;
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
