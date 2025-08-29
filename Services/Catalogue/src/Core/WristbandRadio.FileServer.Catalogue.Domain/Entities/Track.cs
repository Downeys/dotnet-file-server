namespace WristbandRadio.FileServer.Catalogue.Domain.Entities;

public class Track : Entity, IAggregateRoot
{
    public string SongName { get; private set; }
    public string ArtistName { get; private set; }
    public string AlbumName { get; private set; }
    public IEnumerable<string> Genres { get; private set; }
    public string AudioUrl { get; private set; }
    public string AlbumArtUrl { get; private set; }
    public string TrackPurchaseUrl { get; private set; }
    public bool IsExplicit { get; private set; }
    private Track(
        Guid id,
        string songName,
        string artistName,
        string albumName,
        IEnumerable<string> genres,
        string audioUrl,
        string albumArtUrl,
        string trackPurchaseUrl,
        bool isExplicit
    )
    {
        Id = id;
        SongName = songName;
        ArtistName = artistName;
        AlbumName = albumName;
        Genres = genres;
        AudioUrl = audioUrl;
        AlbumArtUrl = albumArtUrl;
        TrackPurchaseUrl = trackPurchaseUrl;
        IsExplicit = isExplicit;
    }
    private Track(
        string songName,
        string artistName,
        string albumName,
        IEnumerable<string> genres,
        string audioUrl,
        string albumArtUrl,
        string trackPurchaseUrl,
        bool isExplicit
    )
    {
        Id = Guid.NewGuid();
        SongName = songName;
        ArtistName = artistName;
        AlbumName = albumName;
        Genres = genres;
        AudioUrl = audioUrl;
        AlbumArtUrl = albumArtUrl;
        TrackPurchaseUrl = trackPurchaseUrl;
        IsExplicit = isExplicit;
    }

    public static Track Create(
        string songName,
        string artistName,
        string albumName,
        IEnumerable<string> genres,
        string audioUrl,
        string albumArtUrl,
        string trackPurchaseUrl,
        bool isExplicit,
        Guid? id = null
    )
    {
        return id.HasValue
            ? new Track(id.Value, songName, artistName, albumName, genres, audioUrl, albumArtUrl, trackPurchaseUrl, isExplicit)
            : new Track(songName, artistName, albumName, genres, audioUrl, albumArtUrl, trackPurchaseUrl, isExplicit);
    }

    public TrackDto ToDto()
    {
        var genreList = Genres.ToList();
        return new TrackDto
        {
            Id = Id,
            SongName = SongName,
            ArtistName = ArtistName,
            AlbumName = AlbumName,
            Genre1 = genreList.Count > 0 ? int.Parse(genreList[0]) : 0,
            Genre2 = genreList.Count > 1 ? int.Parse(genreList[1]) : null,
            Genre3 = genreList.Count > 2 ? int.Parse(genreList[2]) : null,
            Genre4 = genreList.Count > 3 ? int.Parse(genreList[3]) : null,
            Genre5 = genreList.Count > 4 ? int.Parse(genreList[4]) : null,
            AudioUrl = AudioUrl,
            AlbumArtUrl = AlbumArtUrl,
            TrackPurchaseUrl = TrackPurchaseUrl,
            IsExplicit = IsExplicit
        };
    }

    public TrackResponseDto ToResponseDto()
    {
        return new TrackResponseDto(
            Id,
            SongName,
            ArtistName,
            AlbumName,
            Genres,
            AudioUrl,
            AlbumArtUrl,
            TrackPurchaseUrl,
            IsExplicit
        );
    }
}
