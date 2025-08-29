namespace WristbandRadio.FileServer.Catalogue.Domain.Models.Persistence;

[TableName("music.tracks")]
public class TrackDto : IDbEntity
{
    [PrimaryKey]
    [ColumnName("id")]
    public Guid Id { get; set; }
    [ColumnName("song_name")]
    public string SongName { get; set; }
    [ColumnName("artist_name")]
    public string ArtistName { get; set; }
    [ColumnName("album_name")]
    public string AlbumName { get; set; }
    [ColumnName("genre_1")]
    public int Genre1 { get; set; }
    [ColumnName("genre_2")]
    public int? Genre2 { get; set; }
    [ColumnName("genre_3")]
    public int? Genre3 { get; set; }
    [ColumnName("genre_4")]
    public int? Genre4 { get; set; }
    [ColumnName("genre_5")]
    public int? Genre5 { get; set; }
    [ColumnName("audio_url")]
    public string AudioUrl { get; set; }
    [ColumnName("album_art_url")]
    public string AlbumArtUrl { get; set; }
    [ColumnName("track_purchase_url")]
    public string TrackPurchaseUrl { get; set; }
    [ColumnName("is_explicit")]
    public bool IsExplicit { get; set; }
    public TrackDto() { }
}
