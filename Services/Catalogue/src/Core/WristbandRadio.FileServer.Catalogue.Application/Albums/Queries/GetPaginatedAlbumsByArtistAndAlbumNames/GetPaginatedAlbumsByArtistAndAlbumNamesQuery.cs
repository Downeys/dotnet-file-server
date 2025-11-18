namespace WristbandRadio.FileServer.Catalogue.Application.Albums.Queries.GetPaginatedAlbumsByArtistAndAlbumNames;

public sealed record GetPaginatedAlbumsByArtistAndAlbumNamesQuery(AlbumQueryParameters QueryParameters, string ArtistName, string AlbumName) : IRequest<PageList<AlbumResponseDto>>;
