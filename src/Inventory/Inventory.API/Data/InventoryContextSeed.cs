using Inventory.API.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Data
{
    public class InventoryContextSeed
    {

        public static async Task SeedAsync(InventoryContext inventoryContext, ILogger<InventoryContextSeed> logger)
        {
            if (!inventoryContext.Products.Any())
            {
                inventoryContext.Products.AddRange(GetPreconfiguredOrders());
                await inventoryContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(InventoryContext).Name);
            }
        }

        private static IEnumerable<Product> GetPreconfiguredOrders()
        {
            return new List<Product>
            {
                new Product() {ProductName = "Vehicle", Make = "Mazda", Model = "CX5", Year = "2020", }
            };
        }
    }
}
