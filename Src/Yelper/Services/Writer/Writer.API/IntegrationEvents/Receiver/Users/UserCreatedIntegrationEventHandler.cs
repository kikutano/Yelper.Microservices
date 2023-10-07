using EventBus.Events;
using MediatR;
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
		await _mediator.Send(new CreateUserCommand("at", "name", "avt"));
	}
}
