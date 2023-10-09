using EventBus.Events;
using EventBus.Interfaces;
using RabbitMQEventBus;
using Writer.API.Common.Configurations;

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
		services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
		services.AddSingleton<IEventBus, EventBusRabbitMQ>();

		RegisterAllIntegrationEvents(services);
	}

	private static void RegisterAllIntegrationEvents(IServiceCollection services)
	{
		var integrationEventHandlers = AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(x => x.GetTypes())
			.Where(x => typeof(IIntegrationEventHandler).IsAssignableFrom(x));

		foreach (var eventHandler in integrationEventHandlers)
		{
			services.AddSingleton(eventHandler);
		}
	}
}
