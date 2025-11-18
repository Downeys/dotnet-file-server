namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Commands.UpdateAlbum;

public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, bool>
{
    private readonly ILogger<UpdateAlbumCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAlbumCommandHandler(ILogger<UpdateAlbumCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }

    public async Task<bool> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
    {
        var album = request.AlbumInput;
        var albumEntity = Album.Create(album.ArtistId, album.AlbumName, album.ArtUrl, album.PurchaseUrl, Guid.NewGuid(), request.AlbumId);
        var isValid = await albumEntity.IsValid();
        if (!isValid) return false;
        await _unitOfWork.BeginTransaction();
        await _unitOfWork.Albums.UpdateAsync(albumEntity.ToDto());
        await _unitOfWork.CommitAndCloseConnection();
        return true;
    }
}
