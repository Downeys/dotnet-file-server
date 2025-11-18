namespace WristbandRadio.FileServer.Catalogue.Domain.Models.Persistence;

[TableName("music.artists")]
public class ArtistDto: IDbEntity
{
    [PrimaryKey]
    [ColumnName("id")]
    public Guid Id { get; set; }
    [ColumnName("artist_name")]
    public string ArtistName { get; set; }
    [ColumnName("hometown_zipcode")]
    public string  HometownZipCode { get; set; }
    [ColumnName("current_zipcode")]
    public string CurrentZipCode { get; set; }
    [ColumnName("created_by")]
    public Guid CreatedBy { get; set; }
    [ColumnName("created_datetime")]
    public DateTime? CreatedDatetime { get; set; }

    public ArtistDto() { }
}
