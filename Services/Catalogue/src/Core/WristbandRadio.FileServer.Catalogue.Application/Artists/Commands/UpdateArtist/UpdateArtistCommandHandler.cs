namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Commands.UpdateArtist;

public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand, bool>
{
    private readonly ILogger<UpdateArtistCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateArtistCommandHandler(ILogger<UpdateArtistCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }

    public async Task<bool> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = request.Artist;
        var artistEntity = Artist.Create(artist.ArtistName, artist.HometownZipCode, artist.CurrentZipCode, Guid.NewGuid(), request.ArtistId);
        var isValid = await artistEntity.IsValid();
        if (!isValid) return false;
        await _unitOfWork.BeginTransaction();
        await _unitOfWork.Artists.UpdateAsync(artistEntity.ToDto());
        await _unitOfWork.CommitAndCloseConnection();
        return true;
    }
}
