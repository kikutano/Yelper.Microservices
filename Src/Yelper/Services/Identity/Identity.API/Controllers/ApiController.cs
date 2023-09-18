using ErrorOr;
using Identity.API.Common.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

[ApiController]
public class ApiController : Controller
{
    protected string RequesterAt =>
        User.Claims.Where(x => x.Type == "at").ToList()[0].Value;

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
