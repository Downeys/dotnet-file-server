namespace WristbandRadio.FileServer.Submissions.Presentation.Controllers.V1;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public sealed class SubmissionsController : ControllerBase
{
    private readonly ILogger<SubmissionsController> _logger;
    private readonly ISender _sender;
    public SubmissionsController(ILogger<SubmissionsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [HttpGet]
    public async Task<IActionResult> GetMusicSubmissions([FromQuery]SubmissionQueryParameters queryParameters)
    {
        _logger.LogInformation("Get MusicSubmission method called");
        var musicSubmissions = await _sender.Send(new GetMusicSubmissionsQuery(queryParameters));
        return Ok(musicSubmissions);
    }

    [HttpPost]
    public async Task<IActionResult> AddMusicSubmission(MusicSubmissionInputDto musicSubmissionInput)
    {
        _logger.LogInformation("Post MusicSubmissiono method called");
        var addMusicSubmissionCommand = new AddMusicSubmissionCommand
            (
                musicSubmissionInput.ArtistName,
                musicSubmissionInput.ContactName,
                musicSubmissionInput.ContactEmail,
                musicSubmissionInput.ContactPhone,
                musicSubmissionInput.OwnsRights
            );
        var submissionId = await _sender.Send(addMusicSubmissionCommand);
        return CreatedAtAction("AddMusicSubmission", new { id = submissionId });
    }
}