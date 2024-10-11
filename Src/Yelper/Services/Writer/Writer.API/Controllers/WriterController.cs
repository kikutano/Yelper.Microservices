using EventBus.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Writer.API.IntegrationEvents.Sender;
using Writer.Application.Writers.Commands;
using Writer.Contracts.Writes;
using YelperCommon;

namespace Writer.API.Controllers;

[Route("api/v1/[controller]")]
public class WriterController : YelperApiController
{
	private readonly ISender _sender;
	private readonly IEventBus _eventBus;

	public WriterController(ISender sender, IEventBus eventBus)
	{
		_sender = sender;
		_eventBus = eventBus;
	}

	[HttpPost]
	[Authorize]
	[SwaggerResponse((int)HttpStatusCode.OK)]
	[SwaggerResponse((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> WriteNewYelp(CreateYelpRequest request)
	{
		var commandResult = await _sender
			.Send(new CreateYelpCommand(RequesterUserId, request.Content));

		if (!commandResult.IsError)
		{
			_eventBus.Publish(new YelpCreatedIntegrationEvent(
				Id: commandResult.Value.Id,
				UserId: RequesterUserId,
				Content: request.Content,
				CreatedAt: commandResult.Value.CreatedAt));

			return Ok(commandResult.Value);
		}

		return Problem(commandResult.Errors);
	}
}
