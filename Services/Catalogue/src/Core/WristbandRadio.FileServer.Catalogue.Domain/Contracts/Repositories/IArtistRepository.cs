namespace WristbandRadio.FileServer.Catalogue.Domain.Contracts.Repositories;

public interface IArtistRepository : IGenericRepository<ArtistDto>
{
    Task<PageList<ArtistDto>> GetArtistsAsync(ArtistQueryParameters queryParameters, CancellationToken cancellationToken);
    Task<PageList<ArtistDto>> GetArtistsByNameAndHometownZipCode(ArtistQueryParameters queryParameters, string artistName, string hometownZipCode, CancellationToken cancellationToken);
}
