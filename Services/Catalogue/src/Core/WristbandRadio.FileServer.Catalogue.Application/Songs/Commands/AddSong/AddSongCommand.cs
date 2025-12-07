namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Commands.AddSong;

public sealed record AddSongCommand(SongInputDto SongInput) : IRequest<Guid>;
