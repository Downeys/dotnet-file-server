namespace WristbandRadio.FileServer.Users.Domain.Entities;

public class User : Entity, IAggregateRoot
{
    public string Username { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public Guid CreatedBy { get; private set; }

    private User(string username, string email, Guid createdBy, string? firstName = null, string? lastName = null)
    {
        Id = Guid.NewGuid();
        Username = Guard.Against.NullOrEmpty(username);
        Email = Guard.Against.NullOrEmpty(email);
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
        FirstName = firstName;
        LastName = lastName;
    }
    private User(Guid id, string username, string email, Guid createdBy, string? firstName = null, string? lastName = null)
    {
        Id = Guard.Against.NullOrEmpty(id);
        Username = Guard.Against.NullOrEmpty(username);
        Email = Guard.Against.NullOrEmpty(email);
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
        FirstName = firstName;
        LastName = lastName;
    }
    public static User Create(string username, string email, Guid createdBy, string? firstName = null, string? lastName = null, Guid? id = null)
    {
        return id !=  null
            ? new User(id.Value, username, email, createdBy, firstName, lastName)
            : new User(username, email, createdBy, firstName, lastName);
    }
    public void UpdateName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public async Task<bool> IsValid()
    {
        var validator = new UserValidator(this);
        return await validator.IsValid();
    }

    public UserResponseDto ToResponseDto()
    {
        return new UserResponseDto(
            Id,
            Username,
            Email,
            FirstName ?? string.Empty,
            LastName ?? string.Empty
        );
    }

    public UserDto ToDto()
    {
        return new UserDto
        {
            Id = Id,
            Username = Username,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            CreatedBy = Id,
            CreatedDatetime = DateTime.UtcNow
        };
    }
}
