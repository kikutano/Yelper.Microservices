using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using YelperCommon;

namespace Writer.API.Controllers;

[Route("api/v1/[controller]")]
public class WriterController : YelperApiController
{
    [HttpPost]
    [Authorize]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> WriteNewYelp()
    {
        return Ok();
    }
}
