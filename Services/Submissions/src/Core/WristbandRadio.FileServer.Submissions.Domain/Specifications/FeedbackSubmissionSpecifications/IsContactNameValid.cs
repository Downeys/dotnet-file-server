namespace WristbandRadio.FileServer.Submissions.Domain.Specifications.FeedbackSubmissionSpecifications;

public sealed class IsContactNameValid : ISpecification<FeedbackSubmission>
{
    public bool IsSatisfiedBy(FeedbackSubmission entity)
    {
        var nameRegex = @"^[a-zA-Z\s,.'-]+$";
        return Regex.IsMatch(entity.ContactName, nameRegex, RegexOptions.IgnoreCase);
    }
}
