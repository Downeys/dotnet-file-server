namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Commands.RemoveFeedbackSubmission;

public class RemoveFeedbackSubmissionCommandHandler : IRequestHandler<RemoveFeedbackSubmissionCommand, bool>
{
    private readonly ILogger<RemoveFeedbackSubmissionCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveFeedbackSubmissionCommandHandler(ILogger<RemoveFeedbackSubmissionCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(RemoveFeedbackSubmissionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RemoveFeedbackSubmissionCommand for Id: {Id}", request.Id);
        await _unitOfWork.BeginTransaction();
        await _unitOfWork.FeedbackSubmissions.SoftDeleteAsync(request.Id);
        await _unitOfWork.CommitAndCloseConnection();
        return true;
    }
}
