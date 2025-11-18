namespace WristbandRadio.FileServer.Catalogue.Domain.Entities.Songs;

public class Song : Entity
{
    public string SongName { get; private set; }
    public string AudioUrl { get; private set; }
    public Guid ArtistId { get; private set; }
    public Guid AlbumId { get; private set; }
    public bool IsExplicit { get; private set; }
    public string Status { get; private set; }
    public int DurationInSeconds { get; private set; }
    public int GenreId1 { get; private set; }
    public int? GenreId2 { get; private set; }
    public int? GenreId3 { get; private set; }
    public int? GenreId4 { get; private set; }
    public int? GenreId5 { get; private set; }
    public Guid CreatedBy { get; private set; }

    private Song(
        string songName,
        string audioUrl,
        Guid artistId,
        Guid albumId,
        bool isExplicit,
        int durationInSeconds,
        int genreId1,
        Guid createdBy,
        string? status = "discovery",
        int? genreId2 = null,
        int? genreId3 = null,
        int? genreId4 = null,
        int? genreId5 = null,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        SongName = Guard.Against.NullOrEmpty(songName);
        AudioUrl = Guard.Against.NullOrEmpty(audioUrl);
        ArtistId = Guard.Against.NullOrEmpty(artistId);
        AlbumId = Guard.Against.NullOrEmpty(albumId);
        IsExplicit = isExplicit;
        Status = status ?? "discovery";
        DurationInSeconds = durationInSeconds;
        GenreId1 = genreId1;
        GenreId2 = genreId2;
        GenreId3 = genreId3;
        GenreId4 = genreId4;
        GenreId5 = genreId5;
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
    }

    public Song Create(
        string songName,
        string audioUrl,
        Guid artistId,
        Guid albumId,
        bool isExplicit,
        int durationInSeconds,
        int genreId1,
        Guid createdBy,
        string? status = "discovery",
        int? genreId2 = null,
        int? genreId3 = null,
        int? genreId4 = null,
        int? genreId5 = null,
        Guid? id = null)
    {
        return new Song(songName, audioUrl, artistId, albumId, isExplicit, durationInSeconds, genreId1, createdBy, status, genreId2, genreId3, genreId4, genreId5, id);
    }

    public SongDto ToDto()
    {
        return new SongDto
        {
            Id = Id,
            SongName = SongName,
            AudioUrl = AudioUrl,
            ArtistId = ArtistId,
            AlbumId = AlbumId,
            IsExplicit = IsExplicit,
            Status = Status,
            DurationSeconds = DurationInSeconds,
            Genre1 = GenreId1,
            Genre2 = GenreId2,
            Genre3 = GenreId3,
            Genre4 = GenreId4,
            Genre5 = GenreId5,
            CreatedBy = CreatedBy,
            CreatedDatetime = DateTime.UtcNow
        };
    }

    public SongResponseDto ToResponseDto()
    {
        return new SongResponseDto(Id, SongName, AudioUrl, DurationInSeconds, ArtistId, AlbumId, IsExplicit, GenreId1, GenreId2, GenreId3, GenreId4, GenreId5, Status);
    }
}
