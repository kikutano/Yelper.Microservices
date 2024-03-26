using EventBus.Events;

namespace Identity.API.IntegrationEvents.Sender.Users;

public record UserCreatedIntegrationEvent(
    Guid UserId, string At, string Name, string AvatarUrl) : IntegrationEvent;
