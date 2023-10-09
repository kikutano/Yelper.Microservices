using EventBus.Events;
using MediatR;
using System.Text.Json;
using Writer.Application.Users.Commands;

namespace Writer.API.IntegrationEvents.Receiver.Users;

public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler
{
	private readonly IMediator _mediator;

	public UserCreatedIntegrationEventHandler(IMediator mediator)
	{
		_mediator = mediator;
	}

	public async Task Handle(string plainContent)
	{
		var integrationEvent = JsonSerializer.Deserialize<UserCreatedIntegrationEvent>(plainContent);

		await _mediator.Send(
			new CreateUserCommand(integrationEvent!.At, integrationEvent.Name, integrationEvent.AvatarUrl));
	}
}
