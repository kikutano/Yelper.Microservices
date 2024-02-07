using ErrorOr;
using Identity.Application.Auth.Commands;
using Identity.Application.Auth.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using YelperCommon;

namespace Identity.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : YelperApiController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(AuthRequestResult))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Error))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(Error))]
    public async Task<IActionResult> Auth(AuthRequestCommand request)
    {
        var commandResult = await _mediator.Send(request);

        return commandResult.Match(
            commandResult => Ok(commandResult),
            errors => Problem(errors));
    }

    [HttpGet]
    [Authorize]
    [Route("im_authorized")]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(Error))]
    public IActionResult ImAuthorized()
    {
        return Ok();
    }
}