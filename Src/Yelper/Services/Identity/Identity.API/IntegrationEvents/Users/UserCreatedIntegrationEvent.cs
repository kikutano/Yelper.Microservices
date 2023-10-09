using EventBus.Events;

namespace Identity.API.IntegrationEvents.Users;

public record UserCreatedIntegrationEvent(
	Guid UserId, string At, string Name, string AvatarUrl) : IntegrationEvent;
