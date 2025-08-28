namespace WristbandRadio.FileServer.Submissions.Presentation.Controllers.V1;


[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public sealed class FeedbackSubmissionsController : ControllerBase
{
    private readonly ILogger<FeedbackSubmissionsController> _logger;
    private readonly ISender _sender;
    public FeedbackSubmissionsController(ILogger<FeedbackSubmissionsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [HttpGet]
    public async Task<IActionResult> GetFeedbackSubmissions([FromQuery] SubmissionQueryParameters queryParameters)
    {
        _logger.LogInformation("Get FeedbackSubmission method called");
        var feedbackSubmissions = await _sender.Send(new GetPaginatedFeedbackSubmissionsQuery(queryParameters));
        return Ok(feedbackSubmissions);
    }

    //[HttpGet("music-submission/{id:length(36)}")]
    //public async Task<IActionResult> GetMusicSubmissionById(string id)
    //{
    //    _logger.LogInformation("Get MusicSubmission by Id method called");
    //    var musicSubmission = await _sender.Send(new GetMusicSubmissionByIdQuery(id));
    //    if (musicSubmission == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(musicSubmission.ToResponseDto());
    //}

    //[HttpGet("music-submission/artist/{artistName}")]
    //public async Task<IActionResult> GetMusicSubmissionByArtistName([FromQuery] SubmissionQueryParameters queryParameters, string artistName)
    //{
    //    _logger.LogInformation("Get MusicSubmission by artist name method called");
    //    var musicSubmission = await _sender.Send(new GetPaginatedMusicSubmissionsByArtistNameQuery(queryParameters, artistName));
    //    if (musicSubmission == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(musicSubmission);
    //}

    //[HttpGet("music-submission/status/{status}")]
    //public async Task<IActionResult> GetMusicSubmissionByStatus([FromQuery] SubmissionQueryParameters queryParameters, string status)
    //{
    //    _logger.LogInformation("Get MusicSubmission by status method called");
    //    var musicSubmission = await _sender.Send(new GetPaginatedMusicSubmissionsByStatusQuery(queryParameters, status));
    //    if (musicSubmission == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(musicSubmission);
    //}

    [HttpPost("feedback-submission")]
    public async Task<IActionResult> AddFeedbackSubmission([FromForm] FeedbackSubmissionInputDto feedbackSubmissionInput)
    {
        _logger.LogInformation("Post FeedbackSubmission method called");

        var addFeedbackSubmissionCommand = new AddFeedbackSubmissionCommand
        (
            feedbackSubmissionInput.ContactName,
            feedbackSubmissionInput.ContactEmail,
            feedbackSubmissionInput.ContactPhone,
            feedbackSubmissionInput.FeedbackType,
            feedbackSubmissionInput.FeedbackText
        );
        var submissionId = await _sender.Send(addFeedbackSubmissionCommand);
        return CreatedAtAction("AddFeedbackSubmission", new { id = submissionId });
    }

    //[HttpPut("music-submission/{id:length(36)}/status")]
    //public async Task<IActionResult> UpdateMusicSubmissionStatus(string id, UpdateMusicSubmissionStatusInputDto updateMusicSubmissionStatusInput)
    //{
    //    _logger.LogInformation("Update MusicSubmission status method called");
    //    var updateMusicSubmissionStatusCommand = new UpdateMusicSubmissionStatusCommand
    //        (
    //            Guid.Parse(id),
    //            updateMusicSubmissionStatusInput.Status
    //        );
    //    var result = await _sender.Send(updateMusicSubmissionStatusCommand);
    //    if (!result)
    //    {
    //        return NotFound();
    //    }
    //    return NoContent();
    //}

    //[HttpDelete("music-submission/{id:length(36)}")]
    //public async Task<IActionResult> RemoveMusicSubmission(string id)
    //{
    //    _logger.LogInformation("Delete MusicSubmission method called");
    //    var removeMusicSubmissionCommand = new RemoveMusicSubmissionCommand(Guid.Parse(id));
    //    var result = await _sender.Send(removeMusicSubmissionCommand);
    //    if (!result)
    //    {
    //        return NotFound();
    //    }
    //    return NoContent();
    //}
}
