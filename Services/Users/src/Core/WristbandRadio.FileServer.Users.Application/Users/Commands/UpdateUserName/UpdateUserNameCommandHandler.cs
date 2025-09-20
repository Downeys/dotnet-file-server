namespace WristbandRadio.FileServer.Users.Application.Users.Commands.UpdateUserName;

public class UpdateUserNameCommandHandler : IRequestHandler<UpdateUserNameCommand, bool>
{
    private readonly ILogger<UpdateUserNameCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;
    public UpdateUserNameCommandHandler(ILogger<UpdateUserNameCommandHandler> logger, IUnitOfWork unitOfWork, ISender sender)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
        _sender = Guard.Against.Null(sender);
    }
    public async Task<bool> Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("updating user name");
        var id = Guid.TryParse(request.UserId, out var guid) ? guid : throw new ArgumentException("Invalid ID format", nameof(request.UserId));
        var userEntity = await _sender.Send(new GetUserByIdQuery(request.UserId), cancellationToken);
        if (userEntity == null) throw new UserNotFoundException($"User with id {request.UserId} not found.");
        userEntity.UpdateName(request.FirstName, request.LastName);
        var isValid = await userEntity.IsValid();
        if (!isValid) throw new InvalidUserException("Invalid user update request.");
        await _unitOfWork.BeginTransaction();
        var dto = userEntity.ToDto();
        await _unitOfWork.Users.UpdateAsync(dto);
        await _unitOfWork.CommitAndCloseConnection();
        return true;
    }
}
