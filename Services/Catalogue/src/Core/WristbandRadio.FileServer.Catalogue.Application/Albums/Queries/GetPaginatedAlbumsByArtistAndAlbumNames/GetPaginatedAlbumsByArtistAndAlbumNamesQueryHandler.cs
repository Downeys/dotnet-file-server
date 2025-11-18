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
        var abums = await _unitOfWork.Albums.GetAlbumBuArtistNameAndAlbumName(request.QueryParameters, request.ArtistName, request.AlbumName, cancellationToken);
        if (abums is not null && abums.Items.Any())
        {
            var albumsResponse = abums.Items.Select((album) => Album.Create(album.ArtistId, album.AlbumName, album.AlbumArtUrl, album.AlbumPurchaseUrl, album.CreatedBy).ToResponseDto()).ToList();
            return PageList<AlbumResponseDto>.Create(albumsResponse, abums.PageNumber, abums.PageSize, abums.TotalCount);
        }
        return PageList<AlbumResponseDto>.Create(new List<AlbumResponseDto>(), 1, 0, 0);
    }
}
