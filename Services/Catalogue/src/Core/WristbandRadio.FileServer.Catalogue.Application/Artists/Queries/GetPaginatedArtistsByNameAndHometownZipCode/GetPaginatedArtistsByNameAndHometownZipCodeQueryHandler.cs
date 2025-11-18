namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Queries.GetPaginatedArtistsByNameAndHometownZipCode;

public class GetPaginatedArtistsByNameAndHometownZipCodeQueryHandler : IRequestHandler<GetPaginatedArtistsByNameAndHometownZipCodeQuery, PageList<ArtistResponseDto>>
{
    private readonly ILogger<GetPaginatedArtistsByNameAndHometownZipCodeQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedArtistsByNameAndHometownZipCodeQueryHandler(ILogger<GetPaginatedArtistsByNameAndHometownZipCodeQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }

    public async Task<PageList<ArtistResponseDto>> Handle(GetPaginatedArtistsByNameAndHometownZipCodeQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching artists by name and hometown zipcode");
        var artists = await _unitOfWork.Artists.GetArtistsByNameAndHometownZipCode(request.QueryParameters, request.ArtistName, request.HometownZipCode, cancellationToken);
        if (artists is not null && artists.Items.Any())
        {
            var artistsResponse = artists.Items.Select((artist) => Artist.Create(artist.ArtistName, artist.HometownZipCode, artist.CurrentZipCode, artist.CreatedBy, artist.Id).ToResponseDto()).ToList();
            return PageList<ArtistResponseDto>.Create(artistsResponse, artists.PageNumber, artists.PageSize, artists.TotalCount);
        }
        return PageList<ArtistResponseDto>.Create(new List<ArtistResponseDto>(), 1, 0, 0);
    }
}
