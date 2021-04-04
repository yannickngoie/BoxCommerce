using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Models
{
    public class MessageLog
    {
     public string Id { get; set; }
     public string Sender { get; set; }
     public string QueueName { get; set; }
     public string MessageType { get; set; }

    }
}
