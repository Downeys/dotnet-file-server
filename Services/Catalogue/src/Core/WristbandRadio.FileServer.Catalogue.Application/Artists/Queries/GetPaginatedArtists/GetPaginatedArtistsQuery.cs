namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Queries.GetPaginatedArtists;

public sealed record GetPaginatedArtistsQuery(ArtistQueryParameters QueryParameters) : IRequest<PageList<ArtistResponseDto>>;
