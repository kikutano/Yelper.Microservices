using System.Text.Json.Serialization;

namespace EventBus.Events;

public record IntegrationEvent
{
	public IntegrationEvent()
	{
		Id = Guid.NewGuid();
	}

	[JsonConstructor]
	public IntegrationEvent(Guid id, DateTime createdAt)
	{
		Id = id;
		CreatedAt = createdAt;
	}

	[JsonInclude]
	public Guid Id { get; private init; }

	[JsonInclude]
	public DateTime CreatedAt { get; private init; }
}