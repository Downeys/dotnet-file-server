namespace WristbandRadio.FileServer.Submissions.Domain.Specifications.MusicSubmissionSpecifications;

public sealed class IsOwnershipAttestationChecked : ISpecification<MusicSubmission>
{
    public bool IsSatisfiedBy(MusicSubmission entity)
    {
        return entity.OwnsRights;
    }
}
