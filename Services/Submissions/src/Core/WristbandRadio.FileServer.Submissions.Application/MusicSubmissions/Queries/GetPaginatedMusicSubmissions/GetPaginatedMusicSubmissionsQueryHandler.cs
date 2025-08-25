namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Queries.GetPaginatedMusicSubmissions;

public class GetPaginatedMusicSubmissionsQueryHandler : IRequestHandler<GetPaginatedMusicSubmissionsQuery, PageList<MusicSubmissionResponseDto>>
{
    private readonly ILogger<GetPaginatedMusicSubmissionsQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedMusicSubmissionsQueryHandler(ILogger<GetPaginatedMusicSubmissionsQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<PageList<MusicSubmissionResponseDto>> Handle(GetPaginatedMusicSubmissionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching music submissions");
        var musicSubmissions = await _unitOfWork.MusicSubmissions.GetMusicSubmissionsAsync(request.QueryParameters, cancellationToken);
        if (musicSubmissions != null && musicSubmissions.Items.Any())
        {
            var musicSubmissionResponses = musicSubmissions.Items.Select(ms => MusicSubmission.Create(
                ms.ArtistName,
                ms.ContactName,
                ms.ContactEmail,
                ms.ContactPhone,
                ms.OwnsRights,
                ms.CreatedBy,
                ms.Id,
                ms.Status
            ).ToResponseDto()).ToList();
            return PageList<MusicSubmissionResponseDto>.Create(musicSubmissionResponses, musicSubmissions.PageNumber, musicSubmissions.PageSize, musicSubmissions.TotalCount);
        }
        return PageList<MusicSubmissionResponseDto>.Create(new List<MusicSubmissionResponseDto>(), musicSubmissions?.PageNumber ?? 0, musicSubmissions?.PageSize ?? 0, 0);
    }
}
