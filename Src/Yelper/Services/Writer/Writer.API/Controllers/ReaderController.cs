using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Writer.Application.Writers.Queries;
using YelperCommon;

namespace Writer.API.Controllers;

[Route("api/v1/[controller]")]
public class ReaderController : YelperApiController
{
    private readonly ISender _sender;

    public ReaderController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    //[Authorize("AdminOnly")] Todo
    [Route("{userId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ICollection<YelpItemCollectionResponse>))]
    public async Task<IActionResult> GetYelpsFromUser(Guid userId)
    {
        var result = await _sender.Send(new GetYelpCollectionFromUserQuery(userId));

        return Ok(result);
    }
}
