namespace WristbandRadio.FileServer.Catalogue.Domain.Models.Persistence;

[TableName("music.songs")]
public class SongDto : IDbEntity
{
    [PrimaryKey]
    [ColumnName("id")]
    public Guid Id { get; set; }
    [ColumnName("artist_id")]
    public Guid ArtistId { get; set; }
    [ColumnName("album_id")]
    public Guid AlbumId { get; set; }
    [ColumnName("song_name")]
    public string SongName { get; set; }
    [ColumnName("duration_seconds")]
    public int DurationSeconds { get; set; }
    [ColumnName("audio_url")]
    public string AudioUrl { get; set; }
    [ColumnName("is_explicit")]
    public bool IsExplicit { get; set; }
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
    [ColumnName("status")]
    public string Status { get; set; }
    [ColumnName("created_by")]
    public Guid CreatedBy { get; set; }
    [ColumnName("created_datetime")]
    public DateTime? CreatedDatetime { get; set; }
    public SongDto() { }
}
