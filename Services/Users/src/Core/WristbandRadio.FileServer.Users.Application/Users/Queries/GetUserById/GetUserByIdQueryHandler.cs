namespace WristbandRadio.FileServer.Users.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly ILogger<GetUserByIdQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching user by id {Id}", request.UserId);
        var id = Guid.TryParse(request.UserId, out var guid) ? guid : throw new ArgumentException("Invalid ID format", nameof(request.UserId));

        var user = await _unitOfWork.Users.GetByIdAsync(id, nameof(UserDto.Id), nameof(UserDto.Username), nameof(UserDto.Email), nameof(UserDto.FirstName), nameof(UserDto.LastName), nameof(UserDto.CreatedBy));
        if (user != null)
        {
            return User.Create(user.Username, user.Email, user.CreatedBy, user.FirstName, user.LastName, user.Id);
        }
        return null;
    }
}
