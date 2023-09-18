using ErrorOr;
using Identity.Application.Users.Commands;
using Identity.Application.Users.Common;
using Identity.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Identity.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ApiController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserCreatedResult))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Error))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(Error))]
    public async Task<IActionResult> Create(CreateUserCommand request)
    {
        var commandResult = await _mediator.Send(request);

        if (!commandResult.IsError)
        {
            //_eventBus.Publish(new UserCreatedIntegrationEvent(
            //    UserId: userCreated.UserId,
            //    Identifier: userCreated.Identifier,
            //    Name: userCreated.Name));

            return Ok(commandResult.Value);
        }

        return Problem(commandResult.Errors);
    }

    [HttpGet]
    [Route("{at}")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserResult))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetUserByAt(string at)
    {
        var result = await _mediator.Send(new GetUserByAtQuery(at));

        return Ok(result.Value);
    }
}
