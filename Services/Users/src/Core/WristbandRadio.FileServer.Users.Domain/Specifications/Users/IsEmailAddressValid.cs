namespace WristbandRadio.FileServer.Users.Domain.Specifications.Users;

public sealed class IsEmailAddressValid : ISpecification<User>
{
    public bool IsSatisfiedBy(User entity)
    {
        var emailRegex = @"^[\w-]+@([\w-]+\.)+[\w-]{2,4}$";
        return Regex.IsMatch(entity.Email, emailRegex, RegexOptions.IgnoreCase);
    }
}

