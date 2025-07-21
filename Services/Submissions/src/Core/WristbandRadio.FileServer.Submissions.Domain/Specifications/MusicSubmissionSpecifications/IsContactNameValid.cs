namespace WristbandRadio.FileServer.Submissions.Domain.Specifications.MusicSubmissionSpecifications;

public sealed class IsContactNameValid : ISpecification<MusicSubmission>
{
    public bool IsSatisfiedBy(MusicSubmission entity)
    {
        var nameRegex = @"^[a-zA-Z\s,.'-]+$";
        return Regex.IsMatch(entity.ContactName, nameRegex, RegexOptions.IgnoreCase);
    }
}
