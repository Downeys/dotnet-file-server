namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Queries.GetArtistById;

public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, Artist?>
{
    private readonly ILogger<GetArtistByIdQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public GetArtistByIdQueryHandler(ILogger<GetArtistByIdQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<Artist?> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        var id = Guid.TryParse(request.ArtistId, out var guid) ? guid : throw new ArgumentException("Invalid ID format", nameof(request.ArtistId));
        _logger.LogInformation("fetching artist by id {Id}", request.ArtistId);
        var artist =  await _unitOfWork.Artists.GetByIdAsync(id, nameof(ArtistDto.Id), nameof(ArtistDto.ArtistName), nameof(ArtistDto.HometownZipCode), nameof(ArtistDto.CurrentZipCode), nameof(ArtistDto.CreatedBy));
        if (artist != null)
        {
            var artistResponse = Artist.Create(
                artist.ArtistName,
                artist.HometownZipCode,
                artist.CurrentZipCode,
                Guid.NewGuid(), // should be the id of the user calling the api
                artist.Id
            );
            return artistResponse;
        }
        return null;
    }
}
