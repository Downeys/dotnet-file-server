namespace WristbandRadio.FileServer.Submissions.Domain.Contracts;

public interface IMusicSubmissionRepository : IGenericRepository<MusicSubmissionDto>
{
    public Task<PageList<MusicSubmissionDto>> GetMusicSubmissionsAsync(QueryParameters queryParameters, CancellationToken cancellationToken = default);
}
