namespace WristbandRadio.FileServer.Users.Domain.Validators;

public class UserValidator : IValidator
{
    private readonly User _user;

    public UserValidator(User user)
    {
        _user = Guard.Against.Null(user);
    }

    private Task<bool> ValidateContactEmail()
    {
        var rule = new IsEmailAddressValid();
        var isOk = rule.IsSatisfiedBy(_user);
        if (!isOk) throw new InvalidUserException(EmailInvalid);
        return Task.FromResult(isOk);
    }


    public async Task<bool> IsValid()
    {
        var tasks = new List<Task<bool>>
        {
            ValidateContactEmail()
        };
        var result = await Task.WhenAll(tasks);
        return result.All(x => x);
    }
}
