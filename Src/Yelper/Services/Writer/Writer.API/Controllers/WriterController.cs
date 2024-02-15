using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Writer.Application.Writers.Commands;
using Writer.Contracts.Writes;
using YelperCommon;

namespace Writer.API.Controllers;

[Route("api/v1/[controller]")]
public class WriterController : YelperApiController
{
    private readonly ISender _sender;

    public WriterController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> WriteNewYelp(CreateYelpRequest request)
    {
        var commandResult = await _sender
            .Send(new CreateYelpCommand(RequesterUserId, request.Text));

        return commandResult.Match(
            commandResult => Ok(),
            errors => Problem(errors));
    }
}
