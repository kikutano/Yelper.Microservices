using EventBus.Events;

namespace Writer.API.IntegrationEvents.Receiver.Users;

public record UserCreatedIntegrationEvent(
	Guid UserId, string At, string Name, string AvatarUrl) : IntegrationEvent;
