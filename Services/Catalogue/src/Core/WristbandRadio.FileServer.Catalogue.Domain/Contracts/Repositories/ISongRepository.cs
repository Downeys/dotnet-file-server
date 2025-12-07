namespace WristbandRadio.FileServer.Catalogue.Domain.Contracts.Repositories;

public interface ISongRepository : IGenericRepository<SongDto>
{
    Task<PageList<SongDto>> GetSongsAsync(SongQueryParameters queryParameters, CancellationToken cancellationToken);
    Task<PageList<SongDto>> GetSongsByArtistAlbumAndSongNames(SongQueryParameters queryParameters, string artistName, string albumName, string songName, CancellationToken cancellationToken);
}
