namespace WristbandRadio.FileServer.Users.Application.Users.Queries.GetPaginatedUsers;

public class GetPaginatedUsersQueryHandler : IRequestHandler<GetPaginatedUsersQuery, PageList<UserResponseDto>>
{
    private readonly ILogger<GetPaginatedUsersQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public GetPaginatedUsersQueryHandler(ILogger<GetPaginatedUsersQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<PageList<UserResponseDto>> Handle(GetPaginatedUsersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching users");
        var users = await _unitOfWork.Users.GetAsync(request.QueryParameters, nameof(UserDto.Id), nameof(UserDto.Username), nameof(UserDto.Email), nameof(UserDto.FirstName), nameof(UserDto.LastName), nameof(UserDto.CreatedBy));
        if (users != null && users.Any())
        {
            var userList = users.Select(u => User.Create(
                u.Username,
                u.Email,
                u.CreatedBy,
                u.FirstName,
                u.LastName,
                u.Id
            ).ToResponseDto()).ToList();
            return PageList<UserResponseDto>.Create(userList, request.QueryParameters.PageNo, request.QueryParameters.PageSize, request.QueryParameters.PageSize);
        }
        return PageList<UserResponseDto>.Create(new List<UserResponseDto>(), request.QueryParameters.PageNo, request.QueryParameters.PageSize, request.QueryParameters.PageSize);
    }
}
