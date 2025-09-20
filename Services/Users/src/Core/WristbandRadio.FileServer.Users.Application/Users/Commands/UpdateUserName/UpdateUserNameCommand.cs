namespace WristbandRadio.FileServer.Users.Application.Users.Commands.UpdateUserName;

public sealed record UpdateUserNameCommand(string UserId, string FirstName, string LastName) : IRequest<bool>;
