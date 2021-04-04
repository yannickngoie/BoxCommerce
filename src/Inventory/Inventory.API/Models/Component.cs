using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inventory.API.Models
{
    public class Component
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public bool InStock { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public string ReferenceCode { get; set; }

        public Component (Guid id)
        {
            ID = id;
        }

        public Component()
        {
        }
    }
}
