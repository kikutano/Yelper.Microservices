using EventBus.Events;

namespace Writer.API.IntegrationEvents.Sender;

public record YelpCreatedIntegrationEvent(Guid UserId, string Text) : IntegrationEvent;
