namespace WristbandRadio.FileServer.Catalogue.Infrastructure.Repositories;

public class ArtistRepository : GenericRepository<ArtistDto>, IArtistRepository
{
    public ArtistRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }

    public async Task<PageList<ArtistDto>> GetArtistsAsync(ArtistQueryParameters queryParameters, CancellationToken cancellationToken)
    {
        var artists = await GetAsync(queryParameters, nameof(ArtistDto.Id), nameof(ArtistDto.ArtistName), nameof(ArtistDto.HometownZipCode), nameof(ArtistDto.CurrentZipCode) ,nameof(ArtistDto.CreatedBy));
        var pagedArtists = PageList<ArtistDto>.Create(artists, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedArtists;
    }

    public async Task<PageList<ArtistDto>> GetArtistsByNameAndHometownZipCode(ArtistQueryParameters queryParameters, string artistName, string hometownZipCode, CancellationToken cancellationToken)
    {
        var pageNumber = queryParameters.PageNo;
        var pageSize = queryParameters.PageSize;
        var previousPageLastRecord = (pageNumber - 1) * pageSize;

        var paremeters = new { PreviousPageLastRecord = previousPageLastRecord, PageSize = pageSize, ArtistName = artistName, HometownZipCode = hometownZipCode };
        var sql = $"SELECT id, artist_name, hometown_zipcode, current_zipcode, created_by FROM music.artists WHERE paging_order > @PreviousPageLastRecord AND artist_name = @ArtistName AND hometown_zipcode = HometownZipcCode AND removed_datetime IS NULL ORDER BY paging_order LIMIT @PageSize";

        var connection = await _dapperDataContext?.GetConnection(cancellationToken);
        var submissions = await connection.QueryAsync<ArtistDto>(sql, paremeters);

        var pagedSubmissions = PageList<ArtistDto>.Create(submissions, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedSubmissions;
    }
}
