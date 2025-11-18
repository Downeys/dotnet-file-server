namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Commands.RemoveAlbum;
public sealed record RemoveAlbumCommand(Guid AlbumId) : IRequest<bool>;
