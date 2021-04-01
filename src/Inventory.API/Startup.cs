using Inventory.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Inventory.API;
using Inventory.API.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.Net.Http.Headers;
using MassTransit;
using EventBus.Messages;
using Inventory.API.EventConsumer;
using Inventory.API.Repositories.Interfaces;

namespace Inventory.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<InventoryContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddControllers();
            services.AddScoped<IProductRepository, ProductRepository>();
             services.AddAutoMapper(typeof(Startup));
            services.AddMassTransit(config =>
            {

                config.AddConsumer<OrderConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                    cfg.UseHealthCheck(ctx);

                    cfg.ReceiveEndpoint(EventBusConstants.AvailableStockQueue, c =>
                    {
                        c.ConfigureConsumer<OrderConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();

          
            services.AddScoped<OrderConsumer>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();

                    });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory.API v1", Version = "v1" });
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherForecast", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory.API v1"));

                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecast v1"));
                app.UseCors(options => options.AllowAnyOrigin()
           .WithHeaders(HeaderNames.ContentType));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
