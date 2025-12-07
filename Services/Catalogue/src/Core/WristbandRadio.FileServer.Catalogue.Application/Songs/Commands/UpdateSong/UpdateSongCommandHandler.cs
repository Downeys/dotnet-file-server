namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Commands.UpdateSong;

public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommand, bool>
{
    private readonly ILogger<UpdateSongCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateSongCommandHandler(ILogger<UpdateSongCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<bool> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        var song = request.Song;
        var songEntity = Song.Create(song.SongName, song.AudioUrl, song.ArtistId, song.AlbumId, song.IsExplicit, song.DurationInSeconds, song.Genre1, song.AlbumOrder, song.CreatedBy, song.PurchaseUrl, song.Status, song.Genre2, song.Genre3, song.Genre4, song.Genre5, request.SongId);
        var isValid = await songEntity.IsValid();
        if (!isValid) return false;
        await _unitOfWork.BeginTransaction();
        await _unitOfWork.Songs.UpdateAsync(songEntity.ToDto());
        await _unitOfWork.CommitAndCloseConnection();
        return true;
    }
}
