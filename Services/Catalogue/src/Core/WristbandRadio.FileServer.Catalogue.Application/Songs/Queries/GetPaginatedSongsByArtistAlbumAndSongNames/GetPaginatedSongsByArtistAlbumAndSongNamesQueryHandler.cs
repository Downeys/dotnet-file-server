namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Queries.GetPaginatedSongsByArtistAlbumAndSongNames;

public class GetPaginatedSongsByArtistAlbumAndSongNamesQueryHandler : IRequestHandler<GetPaginatedSongsByArtistAlbumAndSongNamesQuery, PageList<SongResponseDto>>
{
    private readonly ILogger<GetPaginatedSongsByArtistAlbumAndSongNamesQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedSongsByArtistAlbumAndSongNamesQueryHandler(ILogger<GetPaginatedSongsByArtistAlbumAndSongNamesQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<PageList<SongResponseDto>> Handle(GetPaginatedSongsByArtistAlbumAndSongNamesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching paginated songs by artist, album, and song names");
        var songs = await _unitOfWork.Songs.GetSongsByArtistAlbumAndSongNames(request.QueryParameters, request.ArtistName, request.AlbumName, request.SongName, cancellationToken);
        if (songs is not null && songs.Items.Any())
        {
            var songsResponse = songs.Items.Select((song) => Song.Create(song.SongName, song.AudioUrl, song.ArtistId, song.AlbumId, song.IsExplicit, song.DurationInSeconds, song.Genre1, song.AlbumOrder, song.CreatedBy, song.TrackPurchaseUrl, song.Status, song.Genre2, song.Genre3, song.Genre4, song.Genre5, song.Id).ToResponseDto()).ToList();
            return PageList<SongResponseDto>.Create(songsResponse, songs.PageNumber, songs.PageSize, songs.TotalCount);
        }
        return PageList<SongResponseDto>.Create(new List<SongResponseDto>(), 1, 0, 0);
    }
}
