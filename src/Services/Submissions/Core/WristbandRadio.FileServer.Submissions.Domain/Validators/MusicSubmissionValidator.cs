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
        var rule = new IsContactNameValid();
        var isOk = rule.IsSatisfiedBy(_musicSubmission);
        if (!isOk) throw new ArgumentException(ContactNameInvalid);
        return Task.FromResult(isOk);
    }
    public async Task<bool> IsValid()
    {
        var tasks = new List<Task<bool>>
        {
            ValidateContactName()
        };
        var result = await Task.WhenAll(tasks);
        return result.All(x => x);
    }
}
