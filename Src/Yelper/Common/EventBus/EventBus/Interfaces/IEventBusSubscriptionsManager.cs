using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Interfaces;

public interface IEventBusSubscriptionsManager
{
    public void AddSubscription<T_Event, T_Handler>()
        where T_Event : IntegrationEvent
        where T_Handler : IIntegrationEventHandler;
    public List<IIntegrationEventHandler>? GetHandlersForEvent(string eventName);
    public void SetServiceScope(IServiceScope serviceScope);
}
