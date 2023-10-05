using EventBus.Interfaces;
using RabbitMQEventBus;
using Writer.API.Common.Configurations;
using Writer.API.IntegrationEvents.Receiver.Users;

namespace Writer.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSwaggerConfiguration(configuration);
        RegisterEventBus(services);

        services.AddCors(options =>
        {
            options.AddPolicy(
            "CorsPolicy",
             builder => builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
        });

        return services;
    }

    private static void RegisterEventBus(IServiceCollection services)
    {
        services.AddScoped<UserCreatedIntegrationEventHandler>();

        //services.AddSingleton<
        //    IEventBusSubscriptionsManager,
        //    InMemoryEventBusSubscriptionsManager>();

        services.AddSingleton<IEventBus, EventBusRabbitMQ>(
            sp =>
            {
                var eventBusSubscriptionsManager = sp
                    .GetRequiredService<IEventBusSubscriptionsManager>();

                var eventBusMgr = new EventBusRabbitMQ(/*eventBusSubscriptionsManager*/);

                RegisterIntegrationEvents(eventBusMgr);
                return eventBusMgr;
            });
    }

    private static void RegisterIntegrationEvents(IEventBus eventBus)
    {
        eventBus.Subscribe<
            UserCreatedIntegrationEvent,
            UserCreatedIntegrationEventHandler>();
    }
}
