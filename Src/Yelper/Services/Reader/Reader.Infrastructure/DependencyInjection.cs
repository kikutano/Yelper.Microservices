using EventBus.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQEventBus;

namespace Reader.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        RegisterEventBus(services, configuration);

        return services;
    }

    private static void RegisterEventBus(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBus, EventBusRabbitMQ>();
        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
    }
}
