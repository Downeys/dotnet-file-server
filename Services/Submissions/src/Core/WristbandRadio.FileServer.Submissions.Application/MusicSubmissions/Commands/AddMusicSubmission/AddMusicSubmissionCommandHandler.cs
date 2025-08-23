namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Commands.AddMusicSubmission;

public class AddMusicSubmissionCommandHandler : IRequestHandler<AddMusicSubmissionCommand, Guid>
{
    private readonly ILogger<AddMusicSubmissionCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AddMusicSubmissionCommandHandler(ILogger<AddMusicSubmissionCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddMusicSubmissionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling AddMusicSubmissionCommand.");
        _unitOfWork.BeginTransaction();
        var  sub = new MusicSubmissionDto();
        sub.ArtistName = request.ArtistName;
        sub.ContactName = request.ContactName;
        sub.ContactEmail = request.ContactEmail;
        sub.ContactPhone = request.ContactPhone;
        var submissionId = await _unitOfWork.MusicSubmissions.AddAsync(sub);
        _unitOfWork.CommitAndCloseConnection();
        return submissionId;
    }
}
