using EventBus.Events;

namespace EventBus.Abstractions;

public interface IEventBus
{
    public void Publish(IntegrationEvent integrationEvent);
    public void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;
    public void Unsubscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;
}
