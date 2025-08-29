namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Queries.GetPaginatedFeedbackSubmissionsByType;

public class GetPaginatedFeedbackSubmissionsByTypeQueryHandler : IRequestHandler<GetPaginatedFeedbackSubmissionsByTypeQuery, PageList<FeedbackSubmissionResponseDto>>
{
    public readonly ILogger<GetPaginatedFeedbackSubmissionsByTypeQueryHandler> _logger;
    public readonly IUnitOfWork _unitOfWork;

    public GetPaginatedFeedbackSubmissionsByTypeQueryHandler(ILogger<GetPaginatedFeedbackSubmissionsByTypeQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<PageList<FeedbackSubmissionResponseDto>> Handle(GetPaginatedFeedbackSubmissionsByTypeQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching feedback submissions by type");
        var feedbackSubmissions = await _unitOfWork.FeedbackSubmissions.GetFeedbackSubmissionsByType(request.QueryParameters, (int)request.SubmissionType, cancellationToken);
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
