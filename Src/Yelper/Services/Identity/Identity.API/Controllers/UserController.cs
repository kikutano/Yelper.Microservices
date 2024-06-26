﻿using ErrorOr;
using EventBus.Interfaces;
using Identity.API.IntegrationEvents.Sender.Users;
using Identity.Application.Users.Commands;
using Identity.Application.Users.Common;
using Identity.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using YelperCommon;

namespace Identity.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : YelperApiController
{
    private readonly IMediator _mediator;
    private readonly IEventBus _eventBus;

    public UserController(IMediator mediator, IEventBus eventBus)
    {
        _mediator = mediator;
        _eventBus = eventBus;
    }

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserCreatedResult))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Error))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(Error))]
    public async Task<IActionResult> Create(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var commandResult = await _mediator.Send(request, cancellationToken);

        if (!commandResult.IsError)
        {
            _eventBus.Publish(new UserCreatedIntegrationEvent(
                UserId: commandResult.Value.User.UserId,
                At: commandResult.Value.User.At,
                Name: commandResult.Value.User.Name,
                AvatarUrl: commandResult.Value.User.AvatarUrl));

            return Ok(commandResult.Value);
        }

        return Problem(commandResult.Errors);
    }

    [HttpGet]
    [Route("{at}")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserResult))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetUserByAt(string at, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserByAtQuery(at), cancellationToken);

        return Ok(result.Value);
    }
}
