namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Commands.UpdateFeedbackSubmissionStatus;

public class UpdateFeedbackSubmissionStatusCommandHandler : IRequestHandler<UpdateFeedbackSubmissionStatusCommand, bool>
{
    private readonly ILogger<UpdateFeedbackSubmissionStatusCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;

    public UpdateFeedbackSubmissionStatusCommandHandler(ILogger<UpdateFeedbackSubmissionStatusCommandHandler> logger, IUnitOfWork unitOfWork, ISender sender)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
        _sender = Guard.Against.Null(sender);
    }
    public async Task<bool> Handle(UpdateFeedbackSubmissionStatusCommand request, CancellationToken cancellationToken)
    {
        var submissionEntity = await _sender.Send(new GetFeedbackSubmissionByIdQuery(request.SubmissionId.ToString()), cancellationToken);

        if (submissionEntity is null)
        {
            _logger.LogWarning("Music submission with id {SubmissionId} not found", request.SubmissionId);
            return false;
        }

        submissionEntity.UpdateStatus(request.Status);
        var isValid = await submissionEntity.IsValid();
        if (!isValid) return false;

        await _unitOfWork.BeginTransaction();
        await _unitOfWork.FeedbackSubmissions.UpdateAsync(submissionEntity.ToDto());
        await _unitOfWork.CommitAndCloseConnection();

        return true;
    }
}
