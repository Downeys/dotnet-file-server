namespace WristbandRadio.FileServer.Streaming.Presentation.Controllers.V1;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TrackStreamingController : ControllerBase
{
    private readonly ILogger<TrackStreamingController> _logger;
    private readonly ISender _sender;
    public TrackStreamingController(ILogger<TrackStreamingController> logger, ISender sender)
    {
        _logger = Guard.Against.Null(logger);
        _sender = Guard.Against.Null(sender);
    }

    [HttpGet("mp3/{id:length(36)}")]
    public async Task<IActionResult> GetMp3StreamById(string id)
    {
        _logger.LogInformation("Get mp3 stream by Id method called");
        var songId = Guid.TryParse(id, out var guid) ? guid : throw new ArgumentException("Invalid ID format", nameof(id));
        var fileStream = await _sender.Send(new GetMp3StreamQuery(songId));
        if (fileStream == null)
        {
            return NotFound();
        }
        return new FileStreamResult(fileStream, "audio/mpeg")
        {
            EnableRangeProcessing = true
        };
    }

    [HttpGet("webm/{id:length(36)}")]
    public async Task<IActionResult> GetWebmStreamById(string id)
    {
        _logger.LogInformation("Get webm stream by Id method called");
        var songId = Guid.TryParse(id, out var guid) ? guid : throw new ArgumentException("Invalid ID format", nameof(id));
        var fileStream = await _sender.Send(new GetWebmStreamQuery(songId));
        if (fileStream == null)
        {
            return NotFound();
        }
        return new FileStreamResult(fileStream, "audio/webm")
        {
            EnableRangeProcessing = true
        };
    }
}
