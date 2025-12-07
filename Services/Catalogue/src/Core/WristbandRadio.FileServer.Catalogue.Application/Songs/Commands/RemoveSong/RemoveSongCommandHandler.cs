namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Commands.RemoveSong;

public class RemoveSongCommandHandler : IRequestHandler<RemoveSongCommand, bool>
{
    private readonly ILogger<RemoveSongCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveSongCommandHandler(ILogger<RemoveSongCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<bool> Handle(RemoveSongCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RemoveSongCommand for Id: {SongId}", request.SongId);
        await _unitOfWork.BeginTransaction();
        await _unitOfWork.Songs.SoftDeleteAsync(request.SongId);
        await _unitOfWork.CommitAndCloseConnection();
        return true;
    }
}
