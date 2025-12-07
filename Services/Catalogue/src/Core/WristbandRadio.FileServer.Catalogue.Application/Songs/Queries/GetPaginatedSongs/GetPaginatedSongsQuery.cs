namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Queries.GetPaginatedSongs;

public sealed record GetPaginatedSongsQuery(SongQueryParameters QueryParameters) : IRequest<PageList<SongResponseDto>>;
