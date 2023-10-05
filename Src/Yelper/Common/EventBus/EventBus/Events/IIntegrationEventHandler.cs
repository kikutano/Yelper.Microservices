namespace EventBus.Events;

public interface IIntegrationEventHandler
{
    public Task Handle(string plainContent);
}