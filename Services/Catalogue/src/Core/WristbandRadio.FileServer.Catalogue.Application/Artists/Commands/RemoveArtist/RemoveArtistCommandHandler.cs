namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Commands.RemoveArtist;

public class RemoveArtistCommandHandler : IRequestHandler<RemoveArtistCommand, bool>
{
    private readonly ILogger<RemoveArtistCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveArtistCommandHandler(ILogger<RemoveArtistCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }

    public async Task<bool> Handle(RemoveArtistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RemoveArtistCommand for Id: {ArtistId}", request.ArtistId);
        await _unitOfWork.BeginTransaction();
        await _unitOfWork.Artists.SoftDeleteAsync(request.ArtistId);
        await _unitOfWork.CommitAndCloseConnection();
        return true;
    }
}
