 namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Commands.UpdateMusicSubmissionStatus;

public sealed class UpdateMusicSubmissionStatusCommandHandler : IRequestHandler<UpdateMusicSubmissionStatusCommand, bool>
{
    private readonly ILogger<UpdateMusicSubmissionStatusCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;

    public UpdateMusicSubmissionStatusCommandHandler(ILogger<UpdateMusicSubmissionStatusCommandHandler> logger, IUnitOfWork unitOfWork, ISender sender)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _sender = sender;
    }

    public async Task<bool> Handle(UpdateMusicSubmissionStatusCommand request, CancellationToken cancellationToken)
    {
        var submissionEntity = await _sender.Send(new GetMusicSubmissionByIdQuery(request.SubmissionId.ToString()), cancellationToken);
        if (submissionEntity is null)
        {
            _logger.LogWarning("Music submission with id {SubmissionId} not found", request.SubmissionId);
            return false;
        }

        submissionEntity.UpdateStatus(request.Status);
        var isValid = await submissionEntity.IsValid();
        if (!isValid) return false;

        await _unitOfWork.BeginTransaction();
        await _unitOfWork.MusicSubmissions.UpdateAsync(submissionEntity.ToDto());
        await _unitOfWork.CommitAndCloseConnection();
        return true;
    }
}
