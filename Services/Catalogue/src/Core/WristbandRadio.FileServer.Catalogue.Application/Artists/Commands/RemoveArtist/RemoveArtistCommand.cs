namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Commands.RemoveArtist;

public sealed record RemoveArtistCommand(Guid ArtistId) : IRequest<bool>;
