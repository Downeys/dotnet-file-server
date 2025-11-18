namespace WristbandRadio.FileServer.Catalogue.Domain.Models.Persistence;

[TableName("music.albums")]
public class AlbumDto : IDbEntity
{
    [PrimaryKey]
    [ColumnName("id")]
    public Guid Id { get; set; }
    [ColumnName("artist_id")]
    public Guid ArtistId { get; set; }
    [ColumnName("album_name")]
    public string AlbumName { get; set; }
    [ColumnName("album_art_url")]
    public string AlbumArtUrl {  get; set; }
    [ColumnName("album_purchase_url")]
    public string AlbumPurchaseUrl { get; set; }
    [ColumnName("created_by")]
    public Guid CreatedBy { get; set; }
    [ColumnName("created_datetime")]
    public DateTime? CreatedDatetime { get; set; }
    public AlbumDto() { }
}
