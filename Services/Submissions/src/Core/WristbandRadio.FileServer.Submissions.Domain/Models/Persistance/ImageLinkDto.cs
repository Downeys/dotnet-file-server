
namespace WristbandRadio.FileServer.Submissions.Domain.Models.Persistance;

[TableName("submissions.music_submission_image_links")]
public class ImageLinkDto : IDbEntity
{
    [PrimaryKey]
    [ColumnName("id")]
    public Guid Id { get; set; }
    [ForeignKey]
    [ColumnName("music_submission_id")]
    public Guid MusicSubmissionId { get; set; }
    [ColumnName("image_url")]
    public string ImageUrl { get; set; }
    [ColumnName("created_by")]
    public Guid CreatedBy { get; set; }
    [ColumnName("created_datetime")]
    public DateTime? CreatedDatetime { get; set; }
    public ImageLinkDto()
    {
    }
}
