using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus;
using EventBus.Abstractions;
using EventBus.EventBusRabbitMQ;
using Kernels.Infrastructure.Alfresco;
using Kernels.Infrastructure.Cache.Redis;
using Kernels.Infrastructure.Serilog;
using mcr_service_post.Domain.Interfaces;
using mcr_service_post.Domain.Service;
using mcr_service_post.Infrastructure.Data;
using mcr_service_post.Infrastructure.IntegrationEvents.EventHandling;
using mcr_service_post.Infrastructure.IntegrationEvents.Events;
using mcr_service_post.Infrastructure.Repositories;
using mcr_service_user.Infrastructure.Data;
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
using ExtensionsSerilog = Kernels.Infrastructure.Serilog.Extensions;

await ExtensionsSerilog.WithSeriLog(async () =>
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.AddSerilog("Post");

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).UseConsoleLifetime();

    builder.Services
    .AddControllers().Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "mcr_service_post", Version = "v1" });
    })

    // DI
    .AddScoped<IPostRepository, PostRepository>()
    .AddScoped<IPostRepository, PostService>()
    .AddScoped<IUnitOfWork, UnitOfWork>()

    // SqlLite
    .AddDbContext<PostDbContext>(options =>
    {
        options.UseSqlite("Data Source=Posts.db");
    })

    // Config RabbitMQ  
    .AddIntegrationServices(builder.Configuration)
    .AddEventBus(builder.Configuration)

    // Redis Cache
    .AddCustomRedisCache(builder.Configuration)

    //Alfresco
    .AddCustomAlfresco(builder.Configuration);



    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mcr_service_post v1"));
        //dbContext.Database.EnsureCreated();
    }
    // Use Serilog 
    app.UseSerilogRequestLogging(); // Intall packages Serilog.AspnetCore

    app.UseRouting();

    app.UseHttpMetrics(); // Prometheus

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapMetrics(); // Prometheus
    });

    app.UseHttpsRedirection();

    //Subscribe RabbitMQ
    CustomExtensionMethods.ConfigureEventBus(app);
    //CustomExtensionMethods.ListenForIntegrationEvents();

    app.Run();
      
});



public static class CustomExtensionMethods
{
    //public static void ListenForIntegrationEvents()
    //{
    //    var factory = new ConnectionFactory();
    //    var connection = factory.CreateConnection();
    //    var channel = connection.CreateModel();
    //    var consumer = new EventingBasicConsumer(channel); 
    //    consumer.Received += (model, ea) =>
    //    {
    //        var body = ea.Body.ToArray(); 
    //    };
    //    channel.BasicConsume(queue: "test",
    //                             autoAck: true,
    //                             consumer: consumer);
    //}
     
    public static void ConfigureEventBus(IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        eventBus.Subscribe<UserMessIntegrationEvent, UserMessIntegrationEventHandler>();
        eventBus.SubscribeDynamic<TestDemoDynamicIntegrationEventHandler>(string.Empty);
    }

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
        services.AddTransient<TestDemoDynamicIntegrationEventHandler>();
        return services;
    }
}
