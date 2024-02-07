using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YelperCommon;

[ApiController]
public class YelperApiController : Controller
{
    protected Guid RequesterUserId =>
        Guid.Parse(User.Claims.Where(x => x.Type == "id").ToList()[0].Value);

    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }

    protected IActionResult Problem(Error error)
    {
        return Problem(new List<Error> { error });
    }
}
