namespace WristbandRadio.FileServer.Users.Domain.Models.Presentation;

public sealed record UserResponseDto(
    Guid Id,
    string Username,
    string Email,
    string FirstName,
    string LastName
);
