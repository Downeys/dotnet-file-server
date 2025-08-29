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
        var pageNumber = queryParameters.PageNo;
        var pageSize = queryParameters.PageSize;
        var previousPageLastRecord = (pageNumber - 1) * pageSize;

        var paremeters = new { PreviousPageLastRecord = previousPageLastRecord, PageSize = pageSize, ColumnValue = submissionTypeid };
        var sql = $"SELECT id, contact_name, contact_email, contact_phone, feedback_text, status, submission_type_id, created_by,created_datetime FROM submissions.feedback_submissions WHERE paging_order > @PreviousPageLastRecord AND submission_type_id = @ColumnValue AND removed_datetime IS NULL ORDER BY paging_order LIMIT @PageSize";

        var connection = await _dapperDataContext?.GetConnection();
        var submissions = await connection.QueryAsync<FeedbackSubmissionDto>(sql, paremeters);

        var pagedSubmissions = PageList<FeedbackSubmissionDto>.Create(submissions, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedSubmissions;
    }
}
