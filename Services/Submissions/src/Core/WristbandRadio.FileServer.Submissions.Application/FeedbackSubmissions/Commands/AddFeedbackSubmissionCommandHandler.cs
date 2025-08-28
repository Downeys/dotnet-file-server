namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Commands;

public class AddFeedbackSubmissionCommandHandler : IRequestHandler<AddFeedbackSubmissionCommand, Guid>
{
    private readonly ILogger<AddFeedbackSubmissionCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AddFeedbackSubmissionCommandHandler(ILogger<AddFeedbackSubmissionCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<Guid> Handle(AddFeedbackSubmissionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("adding feedback submission");
        var submissionEntity = FeedbackSubmission.Create(
            request.ContactName,
            request.ContactEmail,
            request.ContactPhone,
            Enum.Parse<SubmissionType>(request.FeedbackType),
            request.FeedbackText,
            Guid.NewGuid() // This should be the id of the user calling the api
        );
        var isValid = await submissionEntity.IsValid();
        if (!isValid) throw new InvalidSubmissionException("Invalid feedback submission request.");

        await _unitOfWork.BeginTransaction();
        var dto = submissionEntity.ToDto();
        var submissionId = await _unitOfWork.FeedbackSubmissions.AddAsync(dto);
        await _unitOfWork.CommitAndCloseConnection();
        return submissionId;
    }
}
