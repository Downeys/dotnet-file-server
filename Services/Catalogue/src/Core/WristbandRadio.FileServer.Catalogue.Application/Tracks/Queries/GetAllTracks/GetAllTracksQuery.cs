namespace WristbandRadio.FileServer.Catalogue.Application.Tracks.Queries.GetAllTracks;

public sealed record GetAllTracksQuery() : IRequest<IEnumerable<TrackResponseDto>>;

