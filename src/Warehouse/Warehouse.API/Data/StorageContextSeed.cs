using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.API.Models;

namespace Warehouse.API.Data
{
    public class StorageContextSeed
    {
        public static async Task SeedAsync(StorageContext storageContext, ILogger<StorageContextSeed> logger)
        {
            if (!storageContext.Storages.Any())
            {
                storageContext.Storages.AddRange(GetPreconfiguredOrders());
                await storageContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(StorageContext).Name);
            }
        }

        private static IEnumerable<Storage> GetPreconfiguredOrders()
        {
            return new List<Storage>
            {
                new Storage { Name = "Toll",  AddressLine1 = "Address1",  AddressLine2 = "Address1",  City= "JHB", Country = "RSA" }
            };
        }
    }
}
