namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Queries.GetMusicSubmissionById;

public class GetMusicSubmissionByIdQueryHandler : IRequestHandler<GetMusicSubmissionByIdQuery, MusicSubmissionResponseDto?>
{
    private readonly ILogger<GetMusicSubmissionByIdQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetMusicSubmissionByIdQueryHandler(ILogger<GetMusicSubmissionByIdQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<MusicSubmissionResponseDto?> Handle(GetMusicSubmissionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching music submission by id {Id}", request.id);
        var id = Guid.TryParse(request.id, out var guid) ? guid : throw new ArgumentException("Invalid ID format", nameof(request.id));
        var musicSubmission = await _unitOfWork.MusicSubmissions.GetByIdAsync(id, nameof(MusicSubmissionDto.Id), nameof(MusicSubmissionDto.ArtistName), nameof(MusicSubmissionDto.ContactName), nameof(MusicSubmissionDto.ContactEmail), nameof(MusicSubmissionDto.ContactPhone), nameof(MusicSubmissionDto.OwnsRights));
        if (musicSubmission != null)
        {
            var musicSubmissionResponse = new MusicSubmissionResponseDto(
                musicSubmission.Id,
                musicSubmission.ArtistName,
                musicSubmission.ContactName,
                musicSubmission.ContactEmail,
                musicSubmission.ContactPhone,
                musicSubmission.OwnsRights
            );
            return musicSubmissionResponse;
        }
        return null;
    }
}
