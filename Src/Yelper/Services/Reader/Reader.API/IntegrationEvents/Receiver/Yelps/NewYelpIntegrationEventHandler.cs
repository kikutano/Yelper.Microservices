using EventBus.Events;
using MediatR;
using Reader.Application.Yelps.Commands;
using System.Text.Json;

namespace Reader.API.IntegrationEvents.Receiver.Yelps;

public class NewYelpIntegrationEventHandler : IIntegrationEventHandler
{
    private readonly IMediator _mediator;

    public NewYelpIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(string plainContent)
    {
        var integrationEvent = JsonSerializer.Deserialize<NewYelpIntegrationEvent>(plainContent);

        await _mediator.Send(new AddNewYelpToTrendTopicsCommand(
            integrationEvent!.UserId,
            integrationEvent.At,
            integrationEvent.Name,
            integrationEvent.AvatarUrl,
            integrationEvent.Content));
    }
}
