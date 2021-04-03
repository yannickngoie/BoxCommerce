using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Production.API.Models
{
    public class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string ID { get; set; }
        public string OrderID { get; set; }
        public string OrderStatus { get; set; }
        public string OrderNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductID { get; set; }
        public string ProducType { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }

    }
}
