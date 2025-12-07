    namespace WristbandRadio.FileServer.Catalogue.Infrastructure.Repositories;

public class SongRepository : GenericRepository<SongDto>, ISongRepository
{
    public SongRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }
    public async Task<PageList<SongDto>> GetSongsAsync(SongQueryParameters queryParameters, CancellationToken cancellationToken)
    {
        var songs = await GetAsync(queryParameters, nameof(SongDto.Id), nameof(SongDto.ArtistId), nameof(SongDto.AlbumId), nameof(SongDto.SongName), nameof(SongDto.DurationInSeconds), nameof(SongDto.AudioUrl), nameof(SongDto.TrackPurchaseUrl), nameof(SongDto.IsExplicit), nameof(SongDto.Genre1), nameof(SongDto.Genre2), nameof(SongDto.Genre3), nameof(SongDto.Genre4), nameof(SongDto.Genre5), nameof(SongDto.Status), nameof(SongDto.CreatedBy));
        var pagedSongs = PageList<SongDto>.Create(songs, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedSongs;
    }

    public async Task<PageList<SongDto>> GetSongsByArtistAlbumAndSongNames(SongQueryParameters queryParameters, string artistName, string albumName, string songName, CancellationToken cancellationToken)
    {
        var pageNumber = queryParameters.PageNo;
        var pageSize = queryParameters.PageSize;
        var previousPageLastRecord = (pageNumber - 1) * pageSize;

        var paremeters = new { PreviousPageLastRecord = previousPageLastRecord, PageSize = pageSize, SongName = songName, AlbumName = albumName, ArtistName = artistName };
        var sql = $"SELECT s.id, s.artist_id, s.album_id, s.song_name, s.audio_url, s.track_purchase_url, s.genre_1, s.genre_2, s.genre_3, s.genre_4, s.genre_5, s.is_explicit, s.duration_in_seconds, s.status, s.created_by FROM music.songs s LEFT JOIN music.artists art ON s.artist_id = art.id LEFT JOIN music.albums ab ON s.album_id = ab.id WHERE s.paging_order > @PreviousPageLastRecord AND art.artist_name = @ArtistName AND ab.album_name = @AlbumName AND s.song_name = @SongName AND s.removed_datetime IS NULL ORDER BY s.paging_order LIMIT @PageSize";

        var connection = await _dapperDataContext?.GetConnection(cancellationToken);
        var songs = await connection.QueryAsync<SongDto>(sql, paremeters);

        var pagedSongs = PageList<SongDto>.Create(songs, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());
        return pagedSongs;
    }
}
