using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ExceptionHandlerController;

[ApiController]
public class ExceptionController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error() => Problem();

    [Route("/error-development")]
    public IActionResult ErrorDev()
    {
        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }

}