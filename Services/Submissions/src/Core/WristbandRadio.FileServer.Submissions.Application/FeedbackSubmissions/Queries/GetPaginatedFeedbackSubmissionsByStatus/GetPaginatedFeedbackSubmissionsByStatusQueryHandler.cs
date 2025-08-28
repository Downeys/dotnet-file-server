namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Queries.GetPaginatedFeedbackSubmissionsByStatus;

public class GetPaginatedFeedbackSubmissionsByStatusQueryHandler : IRequestHandler<GetPaginatedFeedbackSubmissionsByStatusQuery, PageList<FeedbackSubmissionResponseDto>>
{
    private readonly ILogger<GetPaginatedFeedbackSubmissionsByStatusQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedFeedbackSubmissionsByStatusQueryHandler(ILogger<GetPaginatedFeedbackSubmissionsByStatusQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<PageList<FeedbackSubmissionResponseDto>> Handle(GetPaginatedFeedbackSubmissionsByStatusQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching feedback submissions");
        var feedbackSubmissions = await _unitOfWork.FeedbackSubmissions.GetFeedbackSubmissionsByStatus(request.QueryParameters, request.Status, cancellationToken);
        if (feedbackSubmissions != null && feedbackSubmissions.Items.Any())
        {
            var feedbackSubmissionResponses = feedbackSubmissions.Items.Select(fs => FeedbackSubmission.Create(
                fs.ContactName,
                fs.ContactEmail,
                fs.ContactPhone,
                (SubmissionType)fs.SubmissionTypeId,
                fs.FeedbackText,
                fs.CreatedBy,
                fs.Id,
                fs.Status
            ).ToResponseDto()).ToList();
            return PageList<FeedbackSubmissionResponseDto>.Create(feedbackSubmissionResponses, feedbackSubmissions.PageNumber, feedbackSubmissions.PageSize, feedbackSubmissions.TotalCount);
        }
        return PageList<FeedbackSubmissionResponseDto>.Create(new List<FeedbackSubmissionResponseDto>(), feedbackSubmissions?.PageNumber ?? 0, feedbackSubmissions?.PageSize ?? 0, feedbackSubmissions?.TotalCount ?? 0);
    }
}
