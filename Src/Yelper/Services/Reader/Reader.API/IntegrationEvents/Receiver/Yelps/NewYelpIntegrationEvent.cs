using EventBus.Events;

namespace Reader.API.IntegrationEvents.Receiver.Yelps;

public record NewYelpIntegrationEvent(
    Guid UserId, string At, string Name, string AvatarUrl, string Content) : IntegrationEvent;
