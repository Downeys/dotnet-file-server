namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Services;

public class MusicSubmissionService : IMusicSubmissionService
{
    private readonly ILogger<MusicSubmissionService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MusicSubmissionService(ILogger<MusicSubmissionService> logger, IUnitOfWork unitOfWork)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
    }
    public async Task<Guid> PersistMusicSubmission(MusicSubmission musicSubmission)
    {
        var submissionDto = musicSubmission.ToDto();
        var imageLinkDtos = musicSubmission.GetImageLinkDtos();
        var audioLinkDtos = musicSubmission.GetAudioLinkDtos();
        await _unitOfWork.BeginTransaction();
        var submissionId = await _unitOfWork.MusicSubmissions.AddAsync(submissionDto);
        foreach (var audioLinkDto in audioLinkDtos)
        {
            await _unitOfWork.AudioLinks.AddAsync(audioLinkDto);
        }
        foreach (var imageLinkDto in imageLinkDtos)
        {
            await _unitOfWork.ImageLinks.AddAsync(imageLinkDto);
        }
        await _unitOfWork.CommitAndCloseConnection();
        return submissionId;
    }
}
