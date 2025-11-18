namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Queries.GetAlbumById;

public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, Album?>
{
    private readonly ILogger<GetAlbumByIdQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetAlbumByIdQueryHandler(ILogger<GetAlbumByIdQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }

    public async Task<Album?> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        var id = Guid.TryParse(request.AlbumId, out var guid) ? guid : throw new ArgumentException("Invalid ID format", nameof(request.AlbumId));
        _logger.LogInformation("fetching album by id {Id}", request.AlbumId);
        var album = await _unitOfWork.Albums.GetByIdAsync(id, nameof(AlbumDto.Id), nameof(AlbumDto.AlbumName), nameof(AlbumDto.ArtistId), nameof(AlbumDto.AlbumArtUrl), nameof(AlbumDto.AlbumPurchaseUrl), nameof(AlbumDto.CreatedBy));
        if (album != null)
        {
            var albumResponse = Album.Create(album.ArtistId, album.AlbumName, album.AlbumArtUrl, album.AlbumPurchaseUrl, album.CreatedBy, album.Id);
            return albumResponse;
        }
        return null;
    }
}
