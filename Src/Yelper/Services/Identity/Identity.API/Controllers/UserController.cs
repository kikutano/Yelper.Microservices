using ErrorOr;
using Identity.Application.Users.Commands;
using Identity.Contracts.Users;
using MapsterMapper;
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
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserCreatedDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Error))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(Error))]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        var command = new CreateUserCommand(request.Name, request.Identifier);
        var commandResult = await _mediator.Send(command);

        if (!commandResult.IsError)
        {
            var userCreated = _mapper.Map<UserCreatedDto>(commandResult.Value);

            //_eventBus.Publish(new UserCreatedIntegrationEvent(
            //    UserId: userCreated.UserId,
            //    Identifier: userCreated.Identifier,
            //    Name: userCreated.Name));

            return Ok(userCreated);
        }

        return Problem(commandResult.Errors);
    }
}
