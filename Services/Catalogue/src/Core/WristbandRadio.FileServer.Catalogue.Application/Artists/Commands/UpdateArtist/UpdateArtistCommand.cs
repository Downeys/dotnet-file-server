namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Commands.UpdateArtist;

public sealed record UpdateArtistCommand(Guid ArtistId, ArtistInputDto Artist) : IRequest<bool>;
