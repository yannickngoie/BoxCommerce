using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Models
{
    public class Factory
    {
        public int FactoryId { get; set; }
        [Required]
        public string FactoryName { get; set; }
        public string Description { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
    }
}
