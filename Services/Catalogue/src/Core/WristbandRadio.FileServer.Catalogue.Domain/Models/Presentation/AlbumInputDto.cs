namespace WristbandRadio.FileServer.Catalogue.Domain.Models.Presentation;

public sealed record AlbumInputDto(Guid ArtistId, string AlbumName, string ArtUrl, string PurchaseUrl, Guid CreatedBy);
