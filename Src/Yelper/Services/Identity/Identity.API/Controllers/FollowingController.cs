using ErrorOr;
using Identity.Application.Followings.Commands;
using Identity.Contracts.Followings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Identity.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class FollowingController : ApiController
{
    private readonly IMediator _mediator;

    public FollowingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Error))]
    public async Task<IActionResult> FollowUser(FollowUserRequest request)
    {
        var commandResult = await _mediator.Send(
            new FollowUserCommand(RequesterUserId, request.userId));

        return commandResult.Match(
           commandResult => Ok(), errors => Problem(errors));
    }

    //[HttpDelete]
    //[SwaggerResponse((int)HttpStatusCode.OK)]
    //[SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Error))]
    //public async Task<IActionResult> UnFollowUser(FollowUserCommand request)
    //{
    //    var commandResult = await _mediator.Send(request);

    //    return commandResult.Match(
    //       commandResult => Ok(), errors => Problem(errors));
    //}
}
