using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Order.Infrastructure;
using Order.Application;
using Order.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Order.API.EventConsumer;
using EventBus.Messages;
using Microsoft.Net.Http.Headers;
using GreenPipes;

namespace Order.API
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
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<OrderContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("OrderingConnectionString")));

            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);

            services.AddMassTransit(config =>
            {

                config.AddConsumer<BasketCheckoutConsumer>();
                config.AddConsumer<InventoryStatusConsumer>();
                config.AddConsumer<ProductionStatusConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                    cfg.UseHealthCheck(ctx);

                    cfg.ReceiveEndpoint(EventBusConstants.UpdateOrderQueue, c =>
                    {
                        c.UseMessageRetry(r => r.Immediate(1));
                        c.ConfigureConsumer<InventoryStatusConsumer>(ctx);
                      

                    });

                    cfg.ReceiveEndpoint(EventBusConstants.TriggerProductionQueue, c =>
                    {
                        c.ConfigureConsumer<ProductionStatusConsumer>(ctx);
                        //c.UseMessageRetry(r => r.Immediate(1));
                    });
                });
            });
            services.AddMassTransitHostedService();

            // General Configuration
            services.AddScoped<BasketCheckoutConsumer>();
            services.AddScoped<InventoryStatusConsumer>();
            services.AddScoped<ProductionStatusConsumer>();
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
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.API", Version = "v2" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.API v1"));
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
