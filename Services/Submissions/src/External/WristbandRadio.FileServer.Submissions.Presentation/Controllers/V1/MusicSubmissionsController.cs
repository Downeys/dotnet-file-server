namespace WristbandRadio.FileServer.Submissions.Presentation.Controllers.V1;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public sealed class MusicSubmissionsController : ControllerBase
{
    private readonly ILogger<MusicSubmissionsController> _logger;
    private readonly ISender _sender;
    public MusicSubmissionsController(ILogger<MusicSubmissionsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [HttpGet]
    public async Task<IActionResult> GetMusicSubmissions([FromQuery]SubmissionQueryParameters queryParameters)
    {
        _logger.LogInformation("Get MusicSubmission method called");
        PageList<MusicSubmissionResponseDto> musicSubmissions;
        if (queryParameters.Status != null)
            musicSubmissions = await _sender.Send(new GetPaginatedMusicSubmissionsByStatusQuery(queryParameters, queryParameters.Status));
        else if (queryParameters.ArtistName != null)
            musicSubmissions = await _sender.Send(new GetPaginatedMusicSubmissionsByArtistNameQuery(queryParameters, queryParameters.ArtistName));
        else
            musicSubmissions = await _sender.Send(new GetPaginatedMusicSubmissionsQuery(queryParameters));
        if (musicSubmissions == null)
        {
            return NotFound();
        }
        return Ok(musicSubmissions);
    }

    [HttpGet("music-submission/{id:length(36)}")]
    public async Task<IActionResult> GetMusicSubmissionById(string id)
    {
        _logger.LogInformation("Get MusicSubmission by Id method called");
        var musicSubmission = await _sender.Send(new GetMusicSubmissionByIdQuery(id));
        if (musicSubmission == null)
        {
            return NotFound();
        }
        return Ok(musicSubmission.ToResponseDto());
    }

    [HttpPost("music-submission")]
    public async Task<IActionResult> AddMusicSubmission([FromForm] MusicSubmissionInputDto musicSubmissionInput)
    {
        _logger.LogInformation("Post MusicSubmissiono method called");
        var files = musicSubmissionInput.Files;
        if (files == null || files.Count == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var audioFiles = files
            .Where(file => file.ContentType == "audio/mp3" || file.ContentType == "video/webm" || file.ContentType == "audio/mpeg")
            .Select(file => file.OpenReadStream()).ToList();

        var imageFiles = files
            .Where(file => file.ContentType.StartsWith("image"))
            .Select(file => file.OpenReadStream()).ToList();

        Guard.Against.NullOrEmpty(audioFiles);
        Guard.Against.NullOrEmpty(imageFiles);

        var addMusicSubmissionCommand = new AddMusicSubmissionCommand
            (
                musicSubmissionInput.ArtistName,
                musicSubmissionInput.ContactName,
                musicSubmissionInput.ContactEmail,
                musicSubmissionInput.ContactPhone,
                musicSubmissionInput.OwnsRights,
                audioFiles,
                imageFiles
            );
        var submissionId = await _sender.Send(addMusicSubmissionCommand);
        return CreatedAtAction("AddMusicSubmission", new { id = submissionId });
    }

    [HttpPut("music-submission/{id:length(36)}")]
    public async Task<IActionResult> UpdateMusicSubmissionStatus(string id, UpdateMusicSubmissionStatusInputDto updateMusicSubmissionStatusInput)
    {
        _logger.LogInformation("Update MusicSubmission status method called");
        var updateMusicSubmissionStatusCommand = new UpdateMusicSubmissionStatusCommand
            (
                Guid.Parse(id),
                updateMusicSubmissionStatusInput.Status
            );
        var result = await _sender.Send(updateMusicSubmissionStatusCommand);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("music-submission/{id:length(36)}")]
    public async Task<IActionResult> RemoveMusicSubmission(string id)
    {
        _logger.LogInformation("Delete MusicSubmission method called");
        var removeMusicSubmissionCommand = new RemoveMusicSubmissionCommand(Guid.Parse(id));
        var result = await _sender.Send(removeMusicSubmissionCommand);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}