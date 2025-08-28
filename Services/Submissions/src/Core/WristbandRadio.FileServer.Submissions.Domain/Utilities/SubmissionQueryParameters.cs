namespace WristbandRadio.FileServer.Submissions.Domain.Utilities;

public sealed class SubmissionQueryParameters : QueryParameters
{
    public string? Status { get; set; }
    public string? ArtistName { get; set; }
}
