using ErrorOr;
using Identity.Application.Followers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using YelperCommon;

namespace Identity.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class FollowersController : YelperApiController
{
    private readonly IMediator _mediator;

    public FollowersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [Route("{at}")]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetFollowers(string at)
    {
        var followings = await _mediator.Send(new GetUserFollowersQuery(at));

        return Ok(followings.Value);
    }
}
