namespace WristbandRadio.FileServer.Catalogue.Presentation.Controllers.V1;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TracksController : ControllerBase
{
    private readonly ILogger<TracksController> _logger;
    private readonly ISender _sender;
    public TracksController(ILogger<TracksController> logger, ISender sender)
    {
        _logger = Guard.Against.Null(logger);
        _sender = Guard.Against.Null(sender);
    }

    [HttpGet]
    public async Task<IActionResult> GetTracks()
    {
        _logger.LogInformation("Get FeedbackSubmission method called");
        var tracks = await _sender.Send(new GetAllTracksQuery());
        return Ok(tracks);
    }
}
