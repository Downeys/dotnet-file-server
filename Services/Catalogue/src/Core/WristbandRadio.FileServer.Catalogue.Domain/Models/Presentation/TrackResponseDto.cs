namespace WristbandRadio.FileServer.Catalogue.Domain.Models.Presentation;

public sealed record TrackResponseDto
    (
        Guid Id,
        string SongName,
        string ArtistName,
        string AlbumName,
        IEnumerable<string> Genres,
        string AudioUrl,
        string AlbumArtUrl,
        string TrackPurchaseUrl,
        bool IsExplicit
    );
