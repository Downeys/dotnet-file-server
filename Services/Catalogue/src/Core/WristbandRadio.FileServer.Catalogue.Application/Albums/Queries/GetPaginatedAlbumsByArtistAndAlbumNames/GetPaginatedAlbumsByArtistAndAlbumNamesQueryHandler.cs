namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Queries.GetPaginatedAlbumsByArtistAndAlbumNames;

public class GetPaginatedAlbumsByArtistAndAlbumNamesQueryHandler : IRequestHandler<GetPaginatedAlbumsByArtistAndAlbumNamesQuery, PageList<AlbumResponseDto>>
{
    private readonly ILogger<GetPaginatedAlbumsByArtistAndAlbumNamesQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedAlbumsByArtistAndAlbumNamesQueryHandler(ILogger<GetPaginatedAlbumsByArtistAndAlbumNamesQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<PageList<AlbumResponseDto>> Handle(GetPaginatedAlbumsByArtistAndAlbumNamesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching paginated albums");
        var albums = await _unitOfWork.Albums.GetAlbumByArtistNameAndAlbumName(request.QueryParameters, request.ArtistName, request.AlbumName, cancellationToken);
        if (albums is not null && albums.Items.Any())
        {
            var albumsResponse = albums.Items.Select((album) => Album.Create(album.ArtistId, album.AlbumName, album.AlbumArtUrl, album.AlbumPurchaseUrl, album.CreatedBy).ToResponseDto()).ToList();
            return PageList<AlbumResponseDto>.Create(albumsResponse, albums.PageNumber, albums.PageSize, albums.TotalCount);
        }
        return PageList<AlbumResponseDto>.Create(new List<AlbumResponseDto>(), 1, 0, 0);
    }
}
