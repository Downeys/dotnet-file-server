namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Queries.GetSongById;

public sealed record GetSongByIdQuery(string SongId) : IRequest<Song>;
