namespace WristbandRadio.FileServer.Submissions.Domain.Specifications.FeedbackSubmissionSpecifications;

public sealed class IsPhoneNumberValid : ISpecification<FeedbackSubmission>
{
    public bool IsSatisfiedBy(FeedbackSubmission entity)
    {
        var phoneRegex = @"^(1[ -]?)?\d{3}[ -]?\d{3}[ -]?\d{4}$";
        return Regex.IsMatch(entity.ContactPhone, phoneRegex, RegexOptions.IgnoreCase);
    }
}
