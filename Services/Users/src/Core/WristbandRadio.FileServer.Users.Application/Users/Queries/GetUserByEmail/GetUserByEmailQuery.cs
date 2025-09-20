namespace WristbandRadio.FileServer.Users.Application.Users.Queries.GetUserByEmail;

public sealed record GetUserByEmailQuery(UsersQueryParameters QueryParameters, string Email): IRequest<User?>;
