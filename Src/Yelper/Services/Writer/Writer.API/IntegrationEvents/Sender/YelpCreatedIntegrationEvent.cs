using EventBus.Events;

namespace Writer.API.IntegrationEvents.Sender;

public record YelpCreatedIntegrationEvent(
	Guid Id, Guid UserId, string Content, DateTime CreatedAt) : IntegrationEvent;
