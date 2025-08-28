namespace WristbandRadio.FileServer.Submissions.Infrastructure.Repositories;

public class FeedbackSubmissionRepository : GenericRepository<FeedbackSubmissionDto>, IFeedbackSubmissionRepository
{
    public FeedbackSubmissionRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }

    public async Task<PageList<FeedbackSubmissionDto>> GetFeedbackSubmissionsAsync(QueryParameters queryParameters, CancellationToken cancellationToken = default)
    {
        var submissions = await GetAsync(queryParameters, nameof(FeedbackSubmissionDto.Id), nameof(FeedbackSubmissionDto.ContactName), nameof(FeedbackSubmissionDto.ContactEmail), nameof(FeedbackSubmissionDto.ContactPhone), nameof(FeedbackSubmissionDto.FeedbackText), nameof(FeedbackSubmissionDto.Status), nameof(FeedbackSubmissionDto.SubmissionTypeId), nameof(FeedbackSubmissionDto.CreatedBy));
        var pagedSubmissions = PageList<FeedbackSubmissionDto>.Create(submissions, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedSubmissions;
    }

    public async Task<PageList<FeedbackSubmissionDto>> GetFeedbackSubmissionsByStatus(QueryParameters queryParameters, string status, CancellationToken cancellationToken = default)
    {
        var submissions = await GetBySpecificColumnAsync(queryParameters, nameof(FeedbackSubmissionDto.Status), status, nameof(FeedbackSubmissionDto.Id), nameof(FeedbackSubmissionDto.ContactName), nameof(FeedbackSubmissionDto.ContactEmail), nameof(FeedbackSubmissionDto.ContactPhone), nameof(FeedbackSubmissionDto.FeedbackText), nameof(FeedbackSubmissionDto.Status), nameof(FeedbackSubmissionDto.CreatedBy));
        var pagedSubmissions = PageList<FeedbackSubmissionDto>.Create(submissions, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedSubmissions;
    }

    public async Task<PageList<FeedbackSubmissionDto>> GetFeedbackSubmissionsByType(QueryParameters queryParameters, int submissionTypeid, CancellationToken cancellationToken = default)
    {
        var submissions = await GetBySpecificColumnAsync(queryParameters, nameof(FeedbackSubmissionDto.SubmissionTypeId), $"{submissionTypeid}", nameof(FeedbackSubmissionDto.Id), nameof(FeedbackSubmissionDto.ContactName), nameof(FeedbackSubmissionDto.ContactEmail), nameof(FeedbackSubmissionDto.ContactPhone), nameof(FeedbackSubmissionDto.FeedbackText), nameof(FeedbackSubmissionDto.Status), nameof(FeedbackSubmissionDto.CreatedBy));
        var pagedSubmissions = PageList<FeedbackSubmissionDto>.Create(submissions, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedSubmissions;
    }
}
