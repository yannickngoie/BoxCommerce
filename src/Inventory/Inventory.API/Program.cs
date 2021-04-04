using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Inventory.API.Data; 
using Microsoft.Extensions.DependencyInjection;
using Common.Utilities;

namespace Inventory.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)

               .Build()
               .MigrateDatabase<InventoryContext>((context, services) =>
               {
                   var logger = services.GetService<ILogger<InventoryContextSeed>>();
                   InventoryContextSeed
                        .SeedAsync(context, logger)
                        .Wait();
               })
               .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
