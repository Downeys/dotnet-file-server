namespace WristbandRadio.FileServer.Catalogue.Domain.Models.Presentation;

public sealed record AlbumResponseDto(Guid Id, Guid ArtistId, string Name, string ArtUrl, string PurchaseUrl);
