namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Queries.GetAlbumById;

public sealed record GetAlbumByIdQuery(string AlbumId) : IRequest<Album?>;
