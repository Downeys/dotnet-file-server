namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Queries.GetPaginatedAlbums;

public class GetPaginatedAlbumsQueryHandler : IRequestHandler<GetPaginatedAlbumsQuery, PageList<AlbumResponseDto>>
{
    private readonly ILogger<GetPaginatedAlbumsQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedAlbumsQueryHandler(ILogger<GetPaginatedAlbumsQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<PageList<AlbumResponseDto>> Handle(GetPaginatedAlbumsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching paginated albums.");
        var albums = await _unitOfWork.Albums.GetAlbumsAsync(request.QueryParameters, cancellationToken);
        if (albums is not null && albums.Items.Any())
        {
            var albumsResponse = albums.Items.Select((album) => Album.Create(album.ArtistId, album.AlbumName, album.AlbumArtUrl, album.AlbumPurchaseUrl, album.CreatedBy, album.Id).ToResponseDto()).ToList();
            return PageList<AlbumResponseDto>.Create(albumsResponse, albums.PageNumber, albums.PageSize, albums.TotalCount);
        }
        return PageList<AlbumResponseDto>.Create(new List<AlbumResponseDto>(), 1, 0, 0);
    }
}
