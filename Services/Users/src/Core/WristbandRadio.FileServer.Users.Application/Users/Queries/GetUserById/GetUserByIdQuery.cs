namespace WristbandRadio.FileServer.Users.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(string UserId) : IRequest<User>;
