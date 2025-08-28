namespace WristbandRadio.FileServer.Submissions.Domain.Specifications.FeedbackSubmissionSpecifications;

public sealed class IsEmailAddressValid : ISpecification<FeedbackSubmission>
{
    public bool IsSatisfiedBy(FeedbackSubmission entity)
    {
        var emailRegex = @"^[\w-]+@([\w-]+\.)+[\w-]{2,4}$";
        return Regex.IsMatch(entity.ContactEmail, emailRegex, RegexOptions.IgnoreCase);
    }
}
