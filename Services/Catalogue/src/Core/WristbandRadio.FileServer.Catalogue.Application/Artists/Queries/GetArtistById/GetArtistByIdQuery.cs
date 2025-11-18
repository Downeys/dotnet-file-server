namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Queries.GetArtistById;

public sealed record GetArtistByIdQuery(string ArtistId) : IRequest<Artist?>;
