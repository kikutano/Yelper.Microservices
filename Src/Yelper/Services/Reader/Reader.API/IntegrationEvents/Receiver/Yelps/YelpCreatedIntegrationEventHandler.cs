using EventBus.Events;
using MediatR;
using Reader.Application.Yelps.Commands;
using System.Text.Json;

namespace Reader.API.IntegrationEvents.Receiver.Yelps;

public class YelpCreatedIntegrationEventHandler : IIntegrationEventHandler
{
	private readonly IMediator _mediator;

	public YelpCreatedIntegrationEventHandler(IMediator mediator)
	{
		_mediator = mediator;
	}

	public async Task Handle(string plainContent)
	{
		var integrationEvent = JsonSerializer
			.Deserialize<YelpCreatedIntegrationEvent>(plainContent);

		await _mediator.Send(new ArchiveNewYelpCommand(
			integrationEvent!.Id,
			integrationEvent!.UserId,
			integrationEvent.Content,
			integrationEvent.CreatedAt));
	}
}
