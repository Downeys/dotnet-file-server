namespace WristbandRadio.FileServer.Submissions.Domain.Models.Persistence;

[TableName("submissions.music_submission_audio_links")]
public class AudioLinkDto : IDbEntity
{
    [PrimaryKey]
    [ColumnName("id")]
    public Guid Id { get; set; }
    [ForeignKey]
    [ColumnName("music_submission_id")]
    public Guid MusicSubmissionId { get; set; }
    [ColumnName("audio_url")]
    public string AudioUrl { get; set; }
    [ColumnName("created_by")]
    public Guid CreatedBy { get; set; }
    [ColumnName("created_datetime")]
    public DateTime? CreatedDatetime { get; set; }
    public AudioLinkDto()
    {
    }
}
