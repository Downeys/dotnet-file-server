namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Queries.GetPaginatedAlbums;

public sealed record GetPaginatedAlbumsQuery(AlbumQueryParameters QueryParameters) : IRequest<PageList<AlbumResponseDto>>;
