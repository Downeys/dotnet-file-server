namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Commands.AddSong;

public class AddSongCommandHandler : IRequestHandler<AddSongCommand, Guid>
{
    private readonly ILogger<AddSongCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AddSongCommandHandler(ILogger<AddSongCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<Guid> Handle(AddSongCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("adding song");
        var songInput = request.SongInput;
        var songEntity = Song.Create(songInput.SongName, songInput.AudioUrl, songInput.ArtistId, songInput.AlbumId, songInput.IsExplicit, songInput.DurationInSeconds, songInput.Genre1, songInput.AlbumOrder, songInput.CreatedBy, songInput.PurchaseUrl, songInput.Status, songInput.Genre2, songInput.Genre3, songInput.Genre4, songInput.Genre5);
        var isValid = await songEntity.IsValid();
        if (!isValid) throw new InvalidArtistException("Invalid artist request.");
        await _unitOfWork.BeginTransaction();
        var dto = songEntity.ToDto();
        var songId = await _unitOfWork.Songs.AddAsync(dto);
        await _unitOfWork.CommitAndCloseConnection();
        return songId;
    }
}
