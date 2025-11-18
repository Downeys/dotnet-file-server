namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Commands.AddArtist;

public class AddArtistCommandHandler : IRequestHandler<AddArtistCommand, Guid>
{
    private readonly ILogger<AddArtistCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public AddArtistCommandHandler(ILogger<AddArtistCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<Guid> Handle(AddArtistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("adding artist");
        var artistEntity = Artist.Create(
            request.ArtistName,
            request.HometownZipCode,
            request.CurrentZipCode,
            Guid.NewGuid() // should be the id of the user calling the api
            );
        var isValid = await artistEntity.IsValid();
        if (!isValid) throw new InvalidArtistException("Invalid artist request.");
        await _unitOfWork.BeginTransaction();
        var dto = artistEntity.ToDto();
        var artistId = await _unitOfWork.Artists.AddAsync(dto);
        await _unitOfWork.CommitAndCloseConnection();
        return artistId;
    }
}
