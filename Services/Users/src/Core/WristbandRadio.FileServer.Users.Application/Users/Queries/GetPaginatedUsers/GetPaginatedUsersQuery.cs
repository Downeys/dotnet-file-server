namespace WristbandRadio.FileServer.Users.Application.Users.Queries.GetPaginatedUsers;

public sealed record GetPaginatedUsersQuery(UsersQueryParameters QueryParameters): IRequest<PageList<UserResponseDto>>;
