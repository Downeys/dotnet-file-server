namespace WristbandRadio.FileServer.Submissions.Domain.Specifications.FeedbackSubmissionSpecifications;

public class IsFeedbackTextPresent : ISpecification<FeedbackSubmission>
{
    public bool IsSatisfiedBy(FeedbackSubmission entity)
    {
        return !string.IsNullOrWhiteSpace(entity.FeedbackText);
    }
}
