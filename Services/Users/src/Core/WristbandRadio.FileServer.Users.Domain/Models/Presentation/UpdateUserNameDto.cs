namespace WristbandRadio.FileServer.Users.Domain.Models.Presentation;

public sealed record UpdateUserNameDto(
    string FirstName,
    string LastName
);
