namespace WristbandRadio.FileServer.Catalogue.Domain.Contracts.Repositories;

public interface IAlbumRepository : IGenericRepository<AlbumDto>
{
    Task<PageList<AlbumDto>> GetAlbumsAsync(AlbumQueryParameters queryParameters, CancellationToken cancellationToken);
    Task<PageList<AlbumDto>> GetAlbumBuArtistNameAndAlbumName(AlbumQueryParameters queryParameters, string artistName, string albumName, CancellationToken cancellationToken);
}
