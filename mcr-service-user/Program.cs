using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus;
using EventBus.Abstractions;
using EventBus.EventBusRabbitMQ;
using Kernels.Infrastructure.Serilog;
using mcr_service_user.Domain.Interfaces;
using mcr_service_user.Domain.Services;
using mcr_service_user.Infrastructure.Data;
using mcr_service_user.Infrastructure.IntegrationEvents.EventHandling;
using mcr_service_user.Infrastructure.IntegrationEvents.Events;
using mcr_service_user.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prometheus;
using RabbitMQ.Client;
using Serilog;

await Extensions.WithSeriLog(async () =>
{
   

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.AddSerilog("User");

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).UseConsoleLifetime();
   
    builder.Services
    .AddControllers().Services 
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "mcr_service_user", Version = "v1" });
    })

    // DI
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IUserRepository, UserService>()
    .AddScoped<IUnitOfWork, UnitOfWork>()
    

    // SqlLite
    .AddDbContext<UserDbContext>(options =>
    {
        options.UseSqlite("Data Source=Users.db");
    })

    // Config RabbitMQ 
    .AddIntegrationServices(builder.Configuration)
    .AddEventBus(builder.Configuration);
    

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mcr_service_user v1")); 
    }
     
    // Use Serilog 
    app.UseSerilogRequestLogging(); // Intall packages Serilog.AspnetCore

    app.UseRouting();

    app.UseHttpMetrics(); // Prometheus

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    { 
        endpoints.MapDefaultControllerRoute();
        endpoints.MapControllers();
        endpoints.MapMetrics(); // Prometheus
    });

    var eventBus = app.Services.GetRequiredService<IEventBus>();
    eventBus.Subscribe<UserMessIntegrationEvent, UserMessIntegrationEventHandler>();
     
    app.Run();

    

});



public static class CustomExtensionMethods
{
   
    public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

            var factory = new ConnectionFactory()
            {
                HostName = configuration["EventBusConnection"],
                DispatchConsumersAsync = true
            };

            if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
            {
                factory.UserName = configuration["EventBusUserName"];
            }

            if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
            {
                factory.Password = configuration["EventBusPassword"];
            }

            var retryCount = 5;
            if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
            {
                retryCount = int.Parse(configuration["EventBusRetryCount"]);
            }

            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });


        return services;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

            var retryCount = 5;
            if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
            {
                retryCount = int.Parse(configuration["EventBusRetryCount"]);
            }

            return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        services.AddTransient<UserMessIntegrationEventHandler>();
        return services;
    }

}