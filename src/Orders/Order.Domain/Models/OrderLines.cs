

using Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Domain.Models
{
    public class OrderLines
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string ID { get; set; }
        public string OrderID { get; set; }
        public string Name { get; set; }
        public string ProductID { get; set; }
        public string ProductCode { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string SKU { get; set; }

    }
}
