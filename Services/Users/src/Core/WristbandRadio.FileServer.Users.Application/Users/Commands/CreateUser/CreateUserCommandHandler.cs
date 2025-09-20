namespace WristbandRadio.FileServer.Users.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;
    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUnitOfWork unitOfWork, ISender sender)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
        _sender = Guard.Against.Null(sender);
    }
    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("creating user");
        var userEntity = User.Create(
            request.input.Username,
            request.input.Email,
            Guid.NewGuid() // This should be the ID of the user creating this user, but for now we use a new GUID
        );
        var isValid = await userEntity.IsValid();
        if (!isValid) throw new InvalidUserException("Invalid user creation request.");
        await _unitOfWork.BeginTransaction();
        var dto = userEntity.ToDto();
        var userId = await _unitOfWork.Users.AddAsync(dto);
        await _unitOfWork.CommitAndCloseConnection();
        return await _sender.Send(new GetUserByIdQuery(userId.ToString()));
    }
}
