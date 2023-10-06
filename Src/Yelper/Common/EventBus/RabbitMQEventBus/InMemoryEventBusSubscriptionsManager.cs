using EventBus.Events;
using EventBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMQEventBus;

public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
{
	private readonly Dictionary<string, List<SubscriptionInfo>> _handlers = new();
	public static IServiceScope _servicesScope = null!;

	public void SetServiceScope(IServiceScope serviceScope)
	{
		_servicesScope = serviceScope;
	}

	public void AddSubscription<T_Event, T_Handler>()
		where T_Event : IntegrationEvent
		where T_Handler : IIntegrationEventHandler
	{
		string eventName = typeof(T_Event).Name;

		if (_handlers.ContainsKey(eventName))
		{
			_handlers[eventName].Add(new SubscriptionInfo(typeof(T_Handler)));
		}
		else
		{
			_handlers.Add(eventName, new List<SubscriptionInfo>() {
				new SubscriptionInfo( typeof( T_Handler ) )
			});
		}
	}

	public List<IIntegrationEventHandler>? GetHandlersForEvent(string eventName)
	{
		if (_handlers.ContainsKey(eventName))
		{
			var handlers = _handlers[eventName];
			var integrationEventHandlers = new List<IIntegrationEventHandler>();

			foreach (var handler in handlers)
			{
				integrationEventHandlers.Add(ObjectResolver(handler)!);
			}

			return integrationEventHandlers;
		}

		return null;
	}


	private IIntegrationEventHandler? ObjectResolver(SubscriptionInfo subscriptionInfo)
	{
		return _servicesScope
			.ServiceProvider
			.GetService(subscriptionInfo.HandlerType) as IIntegrationEventHandler;
	}
}
