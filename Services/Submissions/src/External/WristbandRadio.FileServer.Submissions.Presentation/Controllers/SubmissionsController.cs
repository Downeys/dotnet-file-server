using Microsoft.AspNetCore.Mvc;

namespace WristbandRadio.FileServer.Submissions.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SubmissionsController : ControllerBase
{
    private readonly ILogger<SubmissionsController> _logger;
    public SubmissionsController(ILogger<SubmissionsController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Get method called");
        return Ok("This is a placeholder for the submissions endpoint.");
    }
}