namespace WristbandRadio.FileServer.Catalogue.Application.Artists.Commands.AddArtist;

public sealed record AddArtistCommand(string ArtistName, string HometownZipCode, string CurrentZipCode) : IRequest<Guid>;
