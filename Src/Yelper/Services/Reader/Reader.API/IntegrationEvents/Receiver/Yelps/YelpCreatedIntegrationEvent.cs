using EventBus.Events;

namespace Reader.API.IntegrationEvents.Receiver.Yelps;

public record YelpCreatedIntegrationEvent(
	Guid Id, Guid UserId, string Content) : IntegrationEvent;
