namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Queries.GetPaginatedArtists;

public class GetPaginatedArtistsQueryHandler : IRequestHandler<GetPaginatedArtistsQuery, PageList<ArtistResponseDto>>
{
    private readonly ILogger<GetPaginatedArtistsQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedArtistsQueryHandler(ILogger<GetPaginatedArtistsQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }

    public async Task<PageList<ArtistResponseDto>> Handle(GetPaginatedArtistsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching paginated artist");
        var artists = await _unitOfWork.Artists.GetArtistsAsync(request.QueryParameters, cancellationToken);
        if (artists is not null && artists.Items.Any()) {
            var artistsResponse = artists.Items.Select((artist) => Artist.Create(artist.ArtistName, artist.HometownZipCode, artist.CurrentZipCode, artist.CreatedBy, artist.Id).ToResponseDto()).ToList();
            return PageList<ArtistResponseDto>.Create(artistsResponse, artists.PageNumber, artists.PageSize, artists.TotalCount);
        }
        return PageList<ArtistResponseDto>.Create(new List<ArtistResponseDto>(), 1, 0, 0);
    }
}
