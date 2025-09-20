namespace WristbandRadio.FileServer.Users.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(UserInputDto input) : IRequest<User>;
