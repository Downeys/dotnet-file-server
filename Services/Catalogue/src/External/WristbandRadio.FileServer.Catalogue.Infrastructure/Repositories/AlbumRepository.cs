namespace WristbandRadio.FileServer.Catalogue.Infrastructure.Repositories;

public class AlbumRepository : GenericRepository<AlbumDto>, IAlbumRepository
{
    public AlbumRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }

    public async Task<PageList<AlbumDto>> GetAlbumsAsync(AlbumQueryParameters queryParameters, CancellationToken cancellationToken)
    {
        var albums = await GetAsync(queryParameters, nameof(AlbumDto.Id), nameof(AlbumDto.ArtistId), nameof(AlbumDto.AlbumName), nameof(AlbumDto.AlbumArtUrl), nameof(AlbumDto.AlbumPurchaseUrl), nameof(AlbumDto.CreatedBy));
        var pagedAlbums = PageList<AlbumDto>.Create(albums, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedAlbums;
    }

    public async Task<PageList<AlbumDto>> GetAlbumByArtistNameAndAlbumName(AlbumQueryParameters queryParameters, string artistName, string albumName, CancellationToken cancellationToken)
    {
        var pageNumber = queryParameters.PageNo;
        var pageSize = queryParameters.PageSize;
        var previousPageLastRecord = (pageNumber - 1) * pageSize;

        var paremeters = new { PreviousPageLastRecord = previousPageLastRecord, PageSize = pageSize, ArtistName = artistName, AlbumName = albumName };
        var sql = $"SELECT id, artist_id, album_name, album_art_url, album_purchase_url, created_by FROM music.artist_albums WHERE paging_order > @PreviousPageLastRecord AND artist_name = @ArtistName AND album_name = AlbumName AND removed_datetime IS NULL ORDER BY paging_order LIMIT @PageSize";

        var connection = await _dapperDataContext?.GetConnection(cancellationToken);
        var albums = await connection.QueryAsync<AlbumDto>(sql, paremeters);

        var pagedAlbums = PageList<AlbumDto>.Create(albums, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedAlbums;
    }
}
