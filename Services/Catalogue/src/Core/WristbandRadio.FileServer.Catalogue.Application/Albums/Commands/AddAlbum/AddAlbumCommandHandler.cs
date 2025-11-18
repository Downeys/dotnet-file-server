namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Commands.AddAlbum;

public class AddAlbumCommandHandler : IRequestHandler<AddAlbumCommand, Guid>
{
    private readonly ILogger<AddAlbumCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AddAlbumCommandHandler(ILogger<AddAlbumCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }

    public async Task<Guid> Handle(AddAlbumCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("adding  album");
        var albumInput = request.AlbumInput;
        var albumEntity = Album.Create(
            albumInput.ArtistId,
            albumInput.AlbumName,
            albumInput.ArtUrl,
            albumInput.PurchaseUrl,
            albumInput.CreatedBy
            );
        var isValid = await albumEntity.IsValid();
        if (!isValid) throw new InvalidArtistException("Invalid artist request.");
        await _unitOfWork.BeginTransaction();
        var dto = albumEntity.ToDto();
        var albumId = await _unitOfWork.Albums.AddAsync(dto);
        await _unitOfWork.CommitAndCloseConnection();
        return albumId;
    }
}
