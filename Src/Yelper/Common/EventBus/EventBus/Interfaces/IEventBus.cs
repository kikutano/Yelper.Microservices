using EventBus.Events;

namespace EventBus.Interfaces;

public interface IEventBus
{
	void Publish<T_Event>(T_Event integrationEvent);
	void Subscribe<T_Event, T_Handler>()
		where T_Event : IntegrationEvent
		where T_Handler : IIntegrationEventHandler;
}
