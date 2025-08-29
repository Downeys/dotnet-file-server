namespace WristbandRadio.FileServer.Submissions.Domain.Utilities;

public class FeedbackSubmissionQueryParameters : QueryParameters
{
    public string? SubmissionType { get; set; }
    public string? Status { get; set; }
}
