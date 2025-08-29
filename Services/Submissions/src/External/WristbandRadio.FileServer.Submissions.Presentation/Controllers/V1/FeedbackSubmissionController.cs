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
    public async Task<IActionResult> GetFeedbackSubmissions([FromQuery] FeedbackSubmissionQueryParameters queryParameters)
    {
        _logger.LogInformation("Get FeedbackSubmission method called");
        PageList<FeedbackSubmissionResponseDto> feedbackSubmissions;
        if (queryParameters.Status != null)
            feedbackSubmissions = await _sender.Send(new GetPaginatedFeedbackSubmissionsByStatusQuery(queryParameters, queryParameters.Status));
        else if (queryParameters.SubmissionType != null)
            feedbackSubmissions = await _sender.Send(new GetPaginatedFeedbackSubmissionsByTypeQuery(queryParameters, Enum.Parse<SubmissionType>(queryParameters.SubmissionType)));
        else
            feedbackSubmissions = await _sender.Send(new GetPaginatedFeedbackSubmissionsQuery(queryParameters));
        if (feedbackSubmissions is null)
        {
            return NotFound();
        }
        return Ok(feedbackSubmissions);
    }

    [HttpGet("feedback-submission/{id:length(36)}")]
    public async Task<IActionResult> GetFeedbackSubmissionById(string id)
    {
        _logger.LogInformation("Get FeedbackSubmission by Id method called");
        var feedbackSubmission = await _sender.Send(new GetFeedbackSubmissionByIdQuery(id));
        if (feedbackSubmission is null)
        {
            return NotFound();
        }
        return Ok(feedbackSubmission.ToResponseDto());
    }

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

    [HttpPut("feedback-submission/{id:length(36)}")]
    public async Task<IActionResult> UpdateMusicSubmissionStatus(string id, UpdateFeedbackSubmissionInputDto updateFeedbackSubmissionInput)
    {
        _logger.LogInformation("Update FeedbackSubmission status method called");
        var updateFeedbackSubmissionStatusCommand = new UpdateFeedbackSubmissionStatusCommand
            (
                Guid.Parse(id),
                updateFeedbackSubmissionInput.Status
            );
        var result = await _sender.Send(updateFeedbackSubmissionStatusCommand);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

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
