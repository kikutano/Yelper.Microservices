using System.Text.Json.Serialization;

namespace EventBus.Events;

public class IntegrationEvent
{
    [JsonInclude]
    public Guid Id { get; private init; }
    [JsonInclude]
    public DateTime CreationDate { get; private init; }

    [JsonConstructor]
    public IntegrationEvent(Guid id, DateTime creationDate)
    {
        Id = id;
        CreationDate = creationDate;
    }

    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }
}
