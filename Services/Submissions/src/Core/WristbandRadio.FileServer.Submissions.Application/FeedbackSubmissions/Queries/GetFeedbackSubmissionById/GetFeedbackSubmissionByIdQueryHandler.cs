namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Queries.GetFeedbackSubmissionById;

public class GetFeedbackSubmissionByIdQueryHandler : IRequestHandler<GetFeedbackSubmissionByIdQuery, FeedbackSubmission?>
{
    private readonly ILogger<GetFeedbackSubmissionByIdQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetFeedbackSubmissionByIdQueryHandler(ILogger<GetFeedbackSubmissionByIdQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<FeedbackSubmission?> Handle(GetFeedbackSubmissionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching music submission by id {Id}", request.Id);
        var id = Guid.TryParse(request.Id, out var guid) ? guid : throw new ArgumentException("Invalid ID format", nameof(request.Id));
        var feedbackSubmission = await _unitOfWork.FeedbackSubmissions.GetByIdAsync(id, nameof(FeedbackSubmissionDto.Id), nameof(FeedbackSubmissionDto.ContactName), nameof(FeedbackSubmissionDto.ContactEmail), nameof(FeedbackSubmissionDto.ContactPhone), nameof(FeedbackSubmissionDto.FeedbackText), nameof(FeedbackSubmissionDto.Status), nameof(FeedbackSubmissionDto.CreatedBy), nameof(FeedbackSubmissionDto.SubmissionTypeId));
        if (feedbackSubmission != null)
        {
            var feedbackSubmissionResponse = FeedbackSubmission.Create(
                feedbackSubmission.ContactName,
                feedbackSubmission.ContactEmail,
                feedbackSubmission.ContactPhone,
                (SubmissionType)feedbackSubmission.SubmissionTypeId,
                feedbackSubmission.FeedbackText,
                feedbackSubmission.CreatedBy,
                feedbackSubmission.Id,
                feedbackSubmission.Status
            );
            return feedbackSubmissionResponse;
        }
        return null;
    }
}
