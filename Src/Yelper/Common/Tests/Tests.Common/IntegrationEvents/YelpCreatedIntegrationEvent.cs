using EventBus.Events;

namespace Tests.Common.IntegrationEvents;

public record YelpCreatedIntegrationEvent(
    Guid UserId, string At, string Name, string AvatarUrl, string Content) : IntegrationEvent;