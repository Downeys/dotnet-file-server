namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Queries.GetMusicSubmissionById;

public class GetMusicSubmissionByIdQueryHandler : IRequestHandler<GetMusicSubmissionByIdQuery, MusicSubmission?>
{
    private readonly ILogger<GetMusicSubmissionByIdQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetMusicSubmissionByIdQueryHandler(ILogger<GetMusicSubmissionByIdQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<MusicSubmission?> Handle(GetMusicSubmissionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching music submission by id {Id}", request.Id);
        var id = Guid.TryParse(request.Id, out var guid) ? guid : throw new ArgumentException("Invalid ID format", nameof(request.Id));
        var musicSubmission = await _unitOfWork.MusicSubmissions.GetByIdAsync(id, nameof(MusicSubmissionDto.Id), nameof(MusicSubmissionDto.ArtistName), nameof(MusicSubmissionDto.ContactName), nameof(MusicSubmissionDto.ContactEmail), nameof(MusicSubmissionDto.ContactPhone), nameof(MusicSubmissionDto.OwnsRights), nameof(MusicSubmissionDto.Status), nameof(MusicSubmissionDto.CreatedBy));
        if (musicSubmission != null)
        {
            var musicSubmissionResponse = MusicSubmission.Create(
                musicSubmission.ArtistName,
                musicSubmission.ContactName,
                musicSubmission.ContactEmail,
                musicSubmission.ContactPhone,
                musicSubmission.OwnsRights,
                musicSubmission.CreatedBy,
                musicSubmission.Id,
                musicSubmission.Status
            );
            return musicSubmissionResponse;
        }
        return null;
    }
}
