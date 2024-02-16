using EventBus.Events;

namespace Tests.Common.IntegrationEvents;

public record UserCreatedIntegrationEvent(
    Guid UserId, string At, string Name, string AvatarUrl) : IntegrationEvent;
