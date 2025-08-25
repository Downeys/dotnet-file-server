namespace WristbandRadio.FileServer.Submissions.Domain.Specifications.MusicSubmissionSpecifications;

public sealed class IsPhoneNumberValid : ISpecification<MusicSubmission>
{
    public bool IsSatisfiedBy(MusicSubmission entity)
    {
        var phoneRegex = @"^(1[ -]?)?\d{3}[ -]?\d{3}[ -]?\d{4}$";
        return Regex.IsMatch(entity.ContactPhone, phoneRegex, RegexOptions.IgnoreCase);
    }
}
