namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Commands.RemoveSong;

public sealed record RemoveSongCommand(Guid SongId) : IRequest<bool>;
