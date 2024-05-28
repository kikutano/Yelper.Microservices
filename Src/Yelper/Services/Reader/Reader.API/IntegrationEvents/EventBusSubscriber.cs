using EventBus.Interfaces;
using Reader.API.IntegrationEvents.Receiver.Yelps;

namespace Reader.API.IntegrationEvents;

public static class EventBusSubscriber
{
    public static void SubscribeAllEventBus(WebApplication webApplication)
    {
        var eventBus = webApplication.Services.GetRequiredService<IEventBus>();
        webApplication.Services
            .GetService<IEventBusSubscriptionsManager>()!
            .SetServiceScope(webApplication.Services.CreateScope());

        eventBus.Subscribe<NewYelpIntegrationEvent, NewYelpIntegrationEventHandler>();
    }
}
