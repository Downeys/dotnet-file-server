namespace WristbandRadio.FileServer.Submissions.Domain.Specifications.MusicSubmissionSpecifications;

public sealed class IsEmailAddressValid : ISpecification<MusicSubmission>
{
    public bool IsSatisfiedBy(MusicSubmission entity)
    {
        var emailRegex = @"^[\w-]+@([\w-]+\.)+[\w-]{2,4}$";
        return Regex.IsMatch(entity.ContactEmail, emailRegex, RegexOptions.IgnoreCase);
    }
}
