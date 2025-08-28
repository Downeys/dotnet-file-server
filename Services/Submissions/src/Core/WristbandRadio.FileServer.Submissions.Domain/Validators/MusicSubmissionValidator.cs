namespace WristbandRadio.FileServer.Submissions.Domain.Validators;

public class MusicSubmissionValidator : IValidator
{
    private readonly MusicSubmission _musicSubmission;

    public MusicSubmissionValidator(MusicSubmission musicSubmission)
    {
        _musicSubmission = Guard.Against.Null(musicSubmission);
    }

    private Task<bool> ValidateContactName()
    {
        var rule = new MusicSubmissionSpecifications.IsContactNameValid();
        var isOk = rule.IsSatisfiedBy(_musicSubmission);
        if (!isOk) throw new ArgumentException(ContactNameInvalid);
        return Task.FromResult(isOk);
    }

    private Task<bool> ValidateContactEmail()
    {
        var rule = new MusicSubmissionSpecifications.IsEmailAddressValid();
        var isOk = rule.IsSatisfiedBy(_musicSubmission);
        if (!isOk) throw new ArgumentException(ContactEmailInvalid);
        return Task.FromResult(isOk);
    }

    private Task<bool> ValidateContactPhone()
    {
        var rule = new MusicSubmissionSpecifications.IsPhoneNumberValid();
        var isOk = rule.IsSatisfiedBy(_musicSubmission);
        if (!isOk) throw new ArgumentException(ContactPhoneInvalid);
        return Task.FromResult(isOk);
    }

    private Task<bool> ValidateOwnershipAttestation()
    {
        var rule = new MusicSubmissionSpecifications.IsOwnershipAttestationChecked();
        var isOk = rule.IsSatisfiedBy(_musicSubmission);
        if (!isOk) throw new ArgumentException(MissingAttestation);
        return Task.FromResult(isOk);
    }

    public async Task<bool> IsValid()
    {
        var tasks = new List<Task<bool>>
        {
            ValidateContactName(),
            ValidateContactEmail(),
            ValidateContactPhone(),
            ValidateOwnershipAttestation()
        };
        var result = await Task.WhenAll(tasks);
        return result.All(x => x);
    }
}
