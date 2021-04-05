using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.API.Data;

namespace Warehouse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)

               .Build()
               .MigrateDatabase<StorageContext>((context, services) =>
               {
                   var logger = services.GetService<ILogger<StorageContext>>();
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
