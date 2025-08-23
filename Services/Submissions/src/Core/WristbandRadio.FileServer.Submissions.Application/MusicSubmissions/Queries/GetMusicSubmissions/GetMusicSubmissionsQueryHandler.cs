namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Queries.GetMusicSubmissions;

public class GetMusicSubmissionsQueryHandler : IRequestHandler<GetMusicSubmissionsQuery, PageList<MusicSubmissionResponseDto>>
{
    private readonly ILogger<GetMusicSubmissionsQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetMusicSubmissionsQueryHandler(ILogger<GetMusicSubmissionsQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<PageList<MusicSubmissionResponseDto>> Handle(GetMusicSubmissionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching music submissions");
        var musicSubmissions = await _unitOfWork.MusicSubmissions.GetMusicSubmissionsAsync(request.QueryParameters, cancellationToken);
        if (musicSubmissions != null && musicSubmissions.Items.Any())
        {
            var musicSubmissionResponses = musicSubmissions.Items.Select(ms => new MusicSubmissionResponseDto(
                ms.Id,
                ms.ArtistName,
                ms.ContactName,
                ms.ContactEmail,
                ms.ContactPhone,
                ms.OwnsRights
            )).ToList();
            return PageList<MusicSubmissionResponseDto>.Create(musicSubmissionResponses, musicSubmissions.PageNumber, musicSubmissions.PageSize, musicSubmissions.TotalCount);
        }
        return PageList<MusicSubmissionResponseDto>.Create(new List<MusicSubmissionResponseDto>(), musicSubmissions?.PageNumber ?? 0, musicSubmissions?.PageSize ?? 0, 0);
    }
}
