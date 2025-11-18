namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Commands.RemoveAlbum;

public class RemoveAlbumCommandHandler : IRequestHandler<RemoveAlbumCommand, bool>
{
    private readonly ILogger<RemoveAlbumCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAlbumCommandHandler(ILogger<RemoveAlbumCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<bool> Handle(RemoveAlbumCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RemoveAlbumCommand for Id: {ArtistId}", request.AlbumId);
        await _unitOfWork.BeginTransaction();
        await _unitOfWork.Albums.SoftDeleteAsync(request.AlbumId);
        await _unitOfWork.CommitAndCloseConnection();
        return true;
    }
}
