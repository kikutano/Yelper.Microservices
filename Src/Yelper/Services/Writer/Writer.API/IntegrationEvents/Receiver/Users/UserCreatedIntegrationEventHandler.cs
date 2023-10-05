using EventBus.Events;

namespace Writer.API.IntegrationEvents.Receiver.Users;

public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler
{
    public Task Handle(string plainContent)
    {
        int a = 0;

        return Task.CompletedTask;
    }
}
