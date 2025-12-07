namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Commands.UpdateSong;

public sealed record UpdateSongCommand(Guid SongId, SongInputDto Song) : IRequest<bool>;
