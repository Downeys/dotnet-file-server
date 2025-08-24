namespace WristbandRadio.FileServer.Submissions.Infrastructure.Repositories;

public sealed class MusicSubmissionRepository : GenericRepository<MusicSubmissionDto>, IMusicSubmissionRepository
{
    public MusicSubmissionRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }

    public async Task<PageList<MusicSubmissionDto>> GetMusicSubmissionsAsync(QueryParameters queryParameters, CancellationToken cancellationToken = default)
    {
        var submissions = await GetAsync(queryParameters, nameof(MusicSubmissionDto.Id), nameof(MusicSubmissionDto.ArtistName), nameof(MusicSubmissionDto.ContactName), nameof(MusicSubmissionDto.ContactEmail), nameof(MusicSubmissionDto.ContactPhone), nameof(MusicSubmissionDto.OwnsRights));
        var pagedSubmissions = PageList<MusicSubmissionDto>.Create(submissions, queryParameters.PageNo, queryParameters.PageSize, 9999);
        return pagedSubmissions;
    }

    public async Task<PageList<MusicSubmissionDto>> GetMusicSubmissionsByArtistName(QueryParameters queryParameters, string artistName, CancellationToken cancellationToken = default)
    {
        var submissions = await GetBySpecificColumnAsync(queryParameters,nameof(MusicSubmissionDto.ArtistName), artistName, nameof(MusicSubmissionDto.Id), nameof(MusicSubmissionDto.ArtistName), nameof(MusicSubmissionDto.ContactName), nameof(MusicSubmissionDto.ContactEmail), nameof(MusicSubmissionDto.ContactPhone), nameof(MusicSubmissionDto.OwnsRights));
        var pagedSubmissions = PageList<MusicSubmissionDto>.Create(submissions, queryParameters.PageNo, queryParameters.PageSize, 9999);
        return pagedSubmissions;
    }
}
