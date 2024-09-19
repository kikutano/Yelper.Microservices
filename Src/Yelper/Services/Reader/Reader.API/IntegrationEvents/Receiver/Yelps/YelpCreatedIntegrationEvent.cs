using EventBus.Events;

namespace Reader.API.IntegrationEvents.Receiver.Yelps;

public record YelpCreatedIntegrationEvent(
    Guid UserId, string At, string Name, string AvatarUrl, string Content) : IntegrationEvent;
