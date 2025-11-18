namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Commands.UpdateAlbum;

public sealed record UpdateAlbumCommand(Guid AlbumId, AlbumInputDto AlbumInput) : IRequest<bool>;
