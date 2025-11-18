namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Commands.AddAlbum;

public sealed record AddAlbumCommand(AlbumInputDto AlbumInput) : IRequest<Guid>;