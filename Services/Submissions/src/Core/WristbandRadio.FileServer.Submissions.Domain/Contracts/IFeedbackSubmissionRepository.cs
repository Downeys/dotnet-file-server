namespace WristbandRadio.FileServer.Submissions.Domain.Contracts;

public interface IFeedbackSubmissionRepository : IGenericRepository<FeedbackSubmissionDto>
{
    public Task<PageList<FeedbackSubmissionDto>> GetFeedbackSubmissionsAsync(QueryParameters queryParameters, CancellationToken cancellationToken = default);
    public Task<PageList<FeedbackSubmissionDto>> GetFeedbackSubmissionsByStatus(QueryParameters queryParameters, string status, CancellationToken cancellationToken = default);
    public Task<PageList<FeedbackSubmissionDto>> GetFeedbackSubmissionsByType(QueryParameters queryParameters, int statusTypeid, CancellationToken cancellationToken = default);
}
