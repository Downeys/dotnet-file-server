namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Queries.GetPaginatedSongs;

public class GetPaginatedSongsQueryHandler : IRequestHandler<GetPaginatedSongsQuery, PageList<SongResponseDto>>
{
    private readonly ILogger<GetPaginatedSongsQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedSongsQueryHandler(ILogger<GetPaginatedSongsQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }

    public async Task<PageList<SongResponseDto>> Handle(GetPaginatedSongsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching paginated songs.");
        var songs = await _unitOfWork.Songs.GetSongsAsync(request.QueryParameters, cancellationToken);
        if (songs is not null && songs.Items.Any())
        {
            var songsResponse = songs.Items.Select((song) => Song.Create(song.SongName, song.AudioUrl, song.ArtistId, song.AlbumId, song.IsExplicit, song.DurationInSeconds, song.Genre1, song.AlbumOrder, song.CreatedBy, song.TrackPurchaseUrl, song.Status, song.Genre2, song.Genre3, song.Genre4, song.Genre5, song.Id).ToResponseDto()).ToList();
            return PageList<SongResponseDto>.Create(songsResponse, songs.PageNumber, songs.PageSize, songs.TotalCount);
        }
        return PageList<SongResponseDto>.Create(new List<SongResponseDto>(), 1, 0, 0);
    }
}
