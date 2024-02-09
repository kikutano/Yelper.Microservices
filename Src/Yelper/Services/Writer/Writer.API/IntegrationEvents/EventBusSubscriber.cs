using EventBus.Interfaces;
using Writer.API.IntegrationEvents.Receiver.Users;

namespace Writer.API.IntegrationEvents;

public static class EventBusSubscriber
{
    public static void SubscribeAllEventBus(WebApplication webApplication)
    {
        var eventBus = webApplication.Services.GetRequiredService<IEventBus>();
        webApplication.Services
            .GetService<IEventBusSubscriptionsManager>()!
            .SetServiceScope(webApplication.Services.CreateScope());

        eventBus.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
    }
}
