using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus;
using EventBus.Abstractions;
using EventBus.EventBusRabbitMQ;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RabbitMQ.Client;
using ui.web.blazorwasm.IntegrationEvents.EventHandling;
using ui.web.blazorwasm.IntegrationEvents.Events;
using ui_web_blazorwasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
 
builder.ConfigureContainer(new AutofacServiceProviderFactory());


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Config RabbitMQ 
builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["EventBusConnection"],
        DispatchConsumersAsync = true
    };

    if (!string.IsNullOrEmpty(builder.Configuration["EventBusUserName"]))
    {
        factory.UserName = builder.Configuration["EventBusUserName"];
    }

    if (!string.IsNullOrEmpty(builder.Configuration["EventBusPassword"]))
    {
        factory.Password = builder.Configuration["EventBusPassword"];
    }

    var retryCount = 5;
    if (!string.IsNullOrEmpty(builder.Configuration["EventBusRetryCount"]))
    {
        retryCount =int.Parse(builder.Configuration["EventBusRetryCount"]);
    }

    return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);


});


builder.Services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
{
    var subscriptionClientName = builder.Configuration["SubscriptionClientName"];
    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

    var retryCount = 5;
    if (!string.IsNullOrEmpty(builder.Configuration["EventBusRetryCount"]))
    {
        retryCount = int.Parse(builder.Configuration["EventBusRetryCount"]);
    }

    return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
});

builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
builder.Services.AddTransient<UserSendCounterToBlazorIntegrationEventHandler>();

//Subscribe RabbitMQ
//var eventBus = builder.Build().Services.GetRequiredService<IEventBus>();
//eventBus.Subscribe<UserSendCounterToBlazorIntegrationEvent, UserSendCounterToBlazorIntegrationEventHandler>();

await builder.Build().RunAsync();




