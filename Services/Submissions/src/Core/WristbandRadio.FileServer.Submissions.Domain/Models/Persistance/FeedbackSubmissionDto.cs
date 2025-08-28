namespace WristbandRadio.FileServer.Submissions.Domain.Models.Persistance;

[TableName("submissions.feedback_submissions")]
public class FeedbackSubmissionDto : IDbEntity
{
    [PrimaryKey]
    [ColumnName("id")]
    public Guid Id { get; set; }
    [ColumnName("contact_name")]
    public string ContactName { get; set; }
    [ColumnName("contact_email")]
    public string ContactEmail { get; set; }
    [ColumnName("contact_phone")]
    public string ContactPhone { get; set; }
    [ColumnName("submission_type_id")]
    public int SubmissionTypeId { get; set; }
    [ColumnName("feedback_text")]
    public string FeedbackText { get; set; }
    [ColumnName("created_by")]
    public Guid CreatedBy { get; set; }
    [ColumnName("created_datetime")]
    public DateTime? CreatedDatetime { get; set; }
    [ColumnName("status")]
    public string Status { get; set; }

    public FeedbackSubmissionDto()
    {
    }
}
