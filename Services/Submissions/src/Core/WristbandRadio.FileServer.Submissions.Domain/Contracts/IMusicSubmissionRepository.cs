namespace WristbandRadio.FileServer.Submissions.Domain.Contracts;

public interface IMusicSubmissionRepository : IGenericRepository<MusicSubmissionDto>
{
    public Task<PageList<MusicSubmissionDto>> GetMusicSubmissionsAsync(QueryParameters queryParameters, CancellationToken cancellationToken = default);
    public Task<PageList<MusicSubmissionDto>> GetMusicSubmissionsByArtistName(QueryParameters queryParameters, string artistName, CancellationToken cancellationToken = default);
    public Task<PageList<MusicSubmissionDto>> GetMusicSubmissionsByStatus(QueryParameters queryParameters, string status, CancellationToken cancellationToken = default);
}
