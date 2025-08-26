namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Commands.RemoveMusicSubmission;

public class RemoveMusicSubmissionCommandHandler : IRequestHandler<RemoveMusicSubmissionCommand, bool>
{
    private readonly ILogger<RemoveMusicSubmissionCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveMusicSubmissionCommandHandler(ILogger<RemoveMusicSubmissionCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RemoveMusicSubmissionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RemoveMusicSubmissionCommand for Id: {Id}", request.Id);
        await _unitOfWork.BeginTransaction();
        await _unitOfWork.MusicSubmissions.SoftDeleteAsync(request.Id);
        await _unitOfWork.CommitAndCloseConnection();
        return true;
    }
}
