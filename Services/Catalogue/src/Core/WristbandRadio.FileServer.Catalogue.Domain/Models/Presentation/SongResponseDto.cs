namespace WristbandRadio.FileServer.Catalogue.Domain.Models.Presentation;

public sealed record SongResponseDto(
    Guid Id,
    string SongName,
    string AudioUrl,
    int DurationInSeconds,
    Guid ArtistId,
    Guid AlbumId,
    bool IsExplicit,
    int Genre1,
    int? Genre2,
    int? Genre3,
    int? Genre4,
    int? Genre5,
    string Status
    );
