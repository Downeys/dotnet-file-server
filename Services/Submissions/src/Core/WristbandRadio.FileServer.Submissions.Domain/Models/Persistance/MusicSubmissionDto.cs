namespace WristbandRadio.FileServer.Submissions.Domain.Models.Persistance;
[TableName("submissions.music_submissions")]
public class MusicSubmissionDto : IDbEntity
{
    [PrimaryKey]
    [ColumnName("id")]
    public Guid Id { get; set; }
    [ColumnName("artist_name")]
    public string ArtistName { get; set; }
    [ColumnName("contact_name")]
    public string ContactName { get; set; }
    [ColumnName("contact_email")]
    public string ContactEmail { get; set; }
    [ColumnName("contact_phone")]
    public string ContactPhone { get; set; }
    [ColumnName("owns_rights")]
    public bool OwnsRights { get; set; }
    [ColumnName("created_datetime")]
    public DateTime? CreatedDatetime { get; set; }
    [ColumnName("created_by")]
    public Guid CreatedBy { get; set; }

    public MusicSubmissionDto()
    {
    }
}
