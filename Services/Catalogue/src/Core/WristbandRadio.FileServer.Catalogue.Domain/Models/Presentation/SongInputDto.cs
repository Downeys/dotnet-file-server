namespace WristbandRadio.FileServer.Catalogue.Domain.Models.Presentation;

public sealed record SongInputDto(
    string SongName,
    string AudioUrl,
    string? PurchaseUrl,
    int DurationInSeconds,
    Guid ArtistId,
    Guid AlbumId,
    bool IsExplicit,
    int AlbumOrder,
    int Genre1,
    int? Genre2,
    int? Genre3,
    int? Genre4,
    int? Genre5,
    string Status,
    Guid CreatedBy
    );
