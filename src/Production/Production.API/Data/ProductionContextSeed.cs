using Production.API.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Production.API.Data;

namespace Inventory.API.Data
{
    public class ProductionContextSeed
    {

        public static async Task SeedAsync(ProductionContext productionContext, ILogger<ProductionContext> logger)
        {
            if (!productionContext.Activities.Any())
            {
                productionContext.Activities.AddRange(GetPreconfiguredOrders());
                await productionContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ProductionContext).Name);
            }
        }

        private static IEnumerable<Activity> GetPreconfiguredOrders()
        {
            return new List<Activity>
            {
                new Activity() {ProductName = "Vehicle", OrderID = "1", Description = "CX57899FX", ProducType = "", }
            };
        }
    }
}
