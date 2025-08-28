
namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Queries.GetPaginatedFeedbackSubmissions;

public class GetPaginatedFeedbackSubmissionsQueryHandler : IRequestHandler<GetPaginatedFeedbackSubmissionsQuery, PageList<FeedbackSubmissionResponseDto>>
{
    private readonly ILogger<GetPaginatedFeedbackSubmissionsQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedFeedbackSubmissionsQueryHandler(ILogger<GetPaginatedFeedbackSubmissionsQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<PageList<FeedbackSubmissionResponseDto>> Handle(GetPaginatedFeedbackSubmissionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching feedback submissions");
        var feedbackSubmissions =  await _unitOfWork.FeedbackSubmissions.GetFeedbackSubmissionsAsync(request.QueryParameters, cancellationToken);
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
        return PageList<FeedbackSubmissionResponseDto>.Create(new List<FeedbackSubmissionResponseDto>(), feedbackSubmissions.PageNumber, feedbackSubmissions.PageSize, feedbackSubmissions.TotalCount);
    }
}
