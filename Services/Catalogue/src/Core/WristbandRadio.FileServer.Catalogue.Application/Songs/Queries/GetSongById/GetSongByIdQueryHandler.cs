namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Queries.GetSongById;

public class GetSongByIdQueryHandler : IRequestHandler<GetSongByIdQuery, Song>
{
    private readonly ILogger<GetSongByIdQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetSongByIdQueryHandler(ILogger<GetSongByIdQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }

    public async Task<Song> Handle(GetSongByIdQuery request, CancellationToken cancellationToken)
    {
        var id = Guid.TryParse(request.SongId, out var guid) ? guid : throw new ArgumentException("Invalid ID format", nameof(request.SongId));
        _logger.LogInformation("fetching song by id {Id}", request.SongId);
        var song = await _unitOfWork.Songs.GetByIdAsync(id, nameof(SongDto.Id), nameof(SongDto.ArtistId), nameof(SongDto.AlbumId), nameof(SongDto.SongName), nameof(SongDto.DurationInSeconds), nameof(SongDto.AudioUrl), nameof(SongDto.TrackPurchaseUrl), nameof(SongDto.IsExplicit), nameof(SongDto.Genre1), nameof(SongDto.Genre2), nameof(SongDto.Genre3), nameof(SongDto.Genre4), nameof(SongDto.Genre5), nameof(SongDto.Status), nameof(SongDto.CreatedBy));
        if (song != null)
        {
            var songResponse = Song.Create(song.SongName, song.AudioUrl, song.ArtistId, song.AlbumId, song.IsExplicit, song.DurationInSeconds, song.Genre1, song.AlbumOrder, song.CreatedBy, song.TrackPurchaseUrl, song.Status, song.Genre2, song.Genre3, song.Genre4, song.Genre5, song.Id);
            return songResponse;
        }
        return null;
    }
}
