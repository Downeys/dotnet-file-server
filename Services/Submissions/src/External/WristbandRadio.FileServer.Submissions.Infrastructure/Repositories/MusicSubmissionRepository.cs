namespace WristbandRadio.FileServer.Submissions.Infrastructure.Repositories;

public sealed class MusicSubmissionRepository : GenericRepository<MusicSubmissionDto>, IMusicSubmissionRepository
{
    public MusicSubmissionRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }

    public async Task<PageList<MusicSubmissionDto>> GetMusicSubmissionsAsync(QueryParameters queryParameters, CancellationToken cancellationToken = default)
    {
        var submissions = await GetAsync(queryParameters, nameof(MusicSubmissionDto.Id), nameof(MusicSubmissionDto.ArtistName), nameof(MusicSubmissionDto.ContactName), nameof(MusicSubmissionDto.ContactEmail), nameof(MusicSubmissionDto.ContactPhone), nameof(MusicSubmissionDto.OwnsRights), nameof(MusicSubmissionDto.Status), nameof(MusicSubmissionDto.CreatedBy));
        var pagedSubmissions = PageList<MusicSubmissionDto>.Create(submissions, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedSubmissions;
    }

    public async Task<PageList<MusicSubmissionDto>> GetMusicSubmissionsByArtistName(QueryParameters queryParameters, string artistName, CancellationToken cancellationToken = default)
    {
        var submissions = await GetBySpecificColumnAsync(queryParameters,nameof(MusicSubmissionDto.ArtistName), artistName, nameof(MusicSubmissionDto.Id), nameof(MusicSubmissionDto.ArtistName), nameof(MusicSubmissionDto.ContactName), nameof(MusicSubmissionDto.ContactEmail), nameof(MusicSubmissionDto.ContactPhone), nameof(MusicSubmissionDto.OwnsRights), nameof(MusicSubmissionDto.Status), nameof(MusicSubmissionDto.CreatedBy));
        var pagedSubmissions = PageList<MusicSubmissionDto>.Create(submissions, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedSubmissions;
    }

    public async Task<PageList<MusicSubmissionDto>> GetMusicSubmissionsByStatus(QueryParameters queryParameters, string status, CancellationToken cancellationToken = default)
    {
        var submissions = await GetBySpecificColumnAsync(queryParameters, nameof(MusicSubmissionDto.Status), status, nameof(MusicSubmissionDto.Id), nameof(MusicSubmissionDto.ArtistName), nameof(MusicSubmissionDto.ContactName), nameof(MusicSubmissionDto.ContactEmail), nameof(MusicSubmissionDto.ContactPhone), nameof(MusicSubmissionDto.OwnsRights), nameof(MusicSubmissionDto.Status), nameof(MusicSubmissionDto.CreatedBy));
        var pagedSubmissions = PageList<MusicSubmissionDto>.Create(submissions, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedSubmissions;
    }
}
