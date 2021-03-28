using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Domain.Common;
using Orders.Domain.Models;

namespace Order.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<CustomerOrder> GetPreconfiguredOrders()
        {
            return new List<CustomerOrder>
            {
                new CustomerOrder() {UserName = "ngy", FirstName = "Yannick", LastName = "Ngoie", EmailAddress = "fewngoie@gmail.com", AddressLine = "Alandridge", Country = "South Africa", TotalPrice = 350 }
            };
        }
    }
}

