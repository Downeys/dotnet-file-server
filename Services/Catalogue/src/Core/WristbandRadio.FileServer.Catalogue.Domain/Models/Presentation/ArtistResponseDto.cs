namespace WristbandRadio.FileServer.Catalogue.Domain.Models.Presentation;

public sealed record ArtistResponseDto(Guid Id, string Name,string HometownZipCode, string CurrentZipCode);
