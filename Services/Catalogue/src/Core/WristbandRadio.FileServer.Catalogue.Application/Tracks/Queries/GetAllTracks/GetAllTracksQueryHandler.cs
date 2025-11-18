namespace WristbandRadio.FileServer.Catalogue.Application.Tracks.Queries.GetAllTracks;

public class GetAllTracksQueryHandler : IRequestHandler<GetAllTracksQuery, IEnumerable<TrackResponseDto>>
{
    private readonly ILogger<GetAllTracksQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public GetAllTracksQueryHandler(ILogger<GetAllTracksQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<IEnumerable<TrackResponseDto>> Handle(GetAllTracksQuery request, CancellationToken cancellationToken)
    {
        var queryParameters = new QueryParameters();
        var tracks = await _unitOfWork.Tracks.GetAsync(queryParameters, nameof(TrackDto.Id), nameof(TrackDto.SongName), nameof(TrackDto.ArtistName), nameof(TrackDto.AlbumName), nameof(TrackDto.Genre1), nameof(TrackDto.Genre2), nameof(TrackDto.Genre3), nameof(TrackDto.Genre4), nameof(TrackDto.Genre5), nameof(TrackDto.AudioUrl), nameof(TrackDto.AlbumArtUrl), nameof(TrackDto.TrackPurchaseUrl), nameof(TrackDto.IsExplicit));
        return tracks.Select(t => {
            var genreList = new List<string>();
            genreList.Add(((Genre)t.Genre1).ToString());
            if (t.Genre2 != null) genreList.Add(((Genre)t.Genre2).ToString());
            if (t.Genre3 != null) genreList.Add(((Genre)t.Genre3).ToString());
            if (t.Genre4 != null) genreList.Add(((Genre)t.Genre4).ToString());
            if (t.Genre5 != null) genreList.Add(((Genre)t.Genre5).ToString());
            return new TrackResponseDto
            (
                t.Id,
                t.SongName,
                t.ArtistName,
                t.AlbumName,
                genreList,
                t.AudioUrl,
                t.AlbumArtUrl,
                t.TrackPurchaseUrl,
                t.IsExplicit
            );
        });
    }
}
