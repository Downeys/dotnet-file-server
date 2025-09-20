namespace WristbandRadio.FileServer.Users.Application.Users.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User?>
{
    ILogger<GetUserByEmailQueryHandler> _logger;
    IUnitOfWork _unitOfWork;
    public GetUserByEmailQueryHandler(ILogger<GetUserByEmailQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }

    public async Task<User?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Users.GetBySpecificColumnAsync(request.QueryParameters, nameof(User.Email), request.Email, nameof(UserDto.Id), nameof(UserDto.Username), nameof(UserDto.Email), nameof(UserDto.FirstName), nameof(UserDto.LastName), nameof(UserDto.CreatedBy));
        if (users != null && users.Any())
        {
            var user = users.First();
            return User.Create(user.Username, user.Email, user.CreatedBy, user.FirstName, user.LastName, user.Id);
        }
        return null;
    }
}
