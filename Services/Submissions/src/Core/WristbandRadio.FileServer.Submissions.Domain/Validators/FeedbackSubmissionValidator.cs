namespace WristbandRadio.FileServer.Submissions.Domain.Validators;

public class FeedbackSubmissionValidator : IValidator
{
    private readonly FeedbackSubmission _feedbackSubmission;

    public FeedbackSubmissionValidator(FeedbackSubmission feedbackSubmission)
    {
        _feedbackSubmission = Guard.Against.Null(feedbackSubmission);
    }
    private Task<bool> ValidateContactName()
    {
        var rule = new FeedbackSubmissionSpecifications.IsContactNameValid();
        var isOk = rule.IsSatisfiedBy(_feedbackSubmission);
        if (!isOk) throw new InvalidSubmissionException(ContactNameInvalid);
        return Task.FromResult(isOk);
    }

    private Task<bool> ValidateContactEmail()
    {
        var rule = new FeedbackSubmissionSpecifications.IsEmailAddressValid();
        var isOk = rule.IsSatisfiedBy(_feedbackSubmission);
        if (!isOk) throw new InvalidSubmissionException(ContactEmailInvalid);
        return Task.FromResult(isOk);
    }

    private Task<bool> ValidateContactPhone()
    {
        var rule = new FeedbackSubmissionSpecifications.IsPhoneNumberValid();
        var isOk = rule.IsSatisfiedBy(_feedbackSubmission);
        if (!isOk) throw new InvalidSubmissionException(ContactPhoneInvalid);
        return Task.FromResult(isOk);
    }

    private Task<bool> ValidateFeedbackText()
    {
        var rule = new FeedbackSubmissionSpecifications.IsFeedbackTextPresent();
        var isOk = rule.IsSatisfiedBy(_feedbackSubmission);
        if (!isOk) throw new InvalidSubmissionException(MissingFeedbackText);
        return Task.FromResult(isOk);
    }

    public async Task<bool> IsValid()
    {
        var tasks = new List<Task<bool>>
        {
            ValidateContactName(),
            ValidateContactEmail(),
            ValidateContactPhone(),
            ValidateFeedbackText()
        };
        var result = await Task.WhenAll(tasks);
        return result.All(x => x);
    }
}
