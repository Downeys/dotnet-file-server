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
        var submissionEntity = ValidateRequest(request);
        var submissionDto = MapToDto(submissionEntity);
        var submissionId = await PersistSubmission(submissionDto);
        return submissionId;
    }

    private MusicSubmission ValidateRequest(AddMusicSubmissionCommand request)
    {
        var submissionEntity = MusicSubmission.Create(
            request.ArtistName,
            request.ContactName,
            request.ContactEmail,
            request.ContactPhone,
            request.OwnsRights,
            Guid.NewGuid()); // This should be the id of the user calling the api
        if (!submissionEntity.IsValid()) throw new ArgumentException("Invalid music submission request."); // make a better exception
        return submissionEntity;
    }

    private MusicSubmissionDto MapToDto(MusicSubmission musicSubmission)
    {
        return new MusicSubmissionDto
        {
            Id = musicSubmission.Id,
            ArtistName = musicSubmission.ArtistName,
            ContactName = musicSubmission.ContactName,
            ContactEmail = musicSubmission.ContactEmail,
            ContactPhone = musicSubmission.ContactPhone,
            OwnsRights = musicSubmission.OwnsRights,
            CreatedBy = musicSubmission.CreatedBy,
            CreatedDatetime = musicSubmission.CreatedDatetime,
            Status = musicSubmission.Status
        };
    }

    private async Task<Guid> PersistSubmission(MusicSubmissionDto submissionDto)
    {
        await _unitOfWork.BeginTransaction();
        var submissionId = await _unitOfWork.MusicSubmissions.AddAsync(submissionDto);
        await _unitOfWork.CommitAndCloseConnection();
        return submissionId;
    }
}
