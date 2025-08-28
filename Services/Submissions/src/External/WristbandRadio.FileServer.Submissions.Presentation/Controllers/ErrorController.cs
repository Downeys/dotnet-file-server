namespace WristbandRadio.FileServer.Submissions.Presentation.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/error")]
public sealed class ErrorController : ControllerBase
{
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        var (statusCode, message) = exception switch
        {
            IApplicationException appException => ((int)appException.StatusCode, appException.ErrorMessage),
            NotFoundException => (404, exception.Message),
            UnauthorizedAccessException => (401, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };
        return Problem(statusCode: statusCode, title: exception?.Message);
    }
}
