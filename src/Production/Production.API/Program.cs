using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Common.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Inventory.API.Data;
using Microsoft.Extensions.DependencyInjection;
using Production.API.Data;

namespace Production.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
                      CreateHostBuilder(args)

               .Build()
               .MigrateDatabase<ProductionContext>((context, services) =>
               {
                  var logger = services.GetService<ILogger<ProductionContext>>();
                   ProductionContextSeed
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
