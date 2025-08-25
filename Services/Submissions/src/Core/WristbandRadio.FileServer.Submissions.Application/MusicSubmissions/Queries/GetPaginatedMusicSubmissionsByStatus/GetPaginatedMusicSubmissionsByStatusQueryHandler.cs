namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Queries.GetPaginatedMusicSubmissionsByStatus;

public sealed class GetPaginatedMusicSubmissionsByStatusQueryHandler : IRequestHandler<GetPaginatedMusicSubmissionsByStatusQuery, PageList<MusicSubmissionResponseDto>>
{
    private readonly ILogger<GetPaginatedMusicSubmissionsByStatusQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedMusicSubmissionsByStatusQueryHandler(ILogger<GetPaginatedMusicSubmissionsByStatusQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<PageList<MusicSubmissionResponseDto>> Handle(GetPaginatedMusicSubmissionsByStatusQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching music submissions by artist name");
        var musicSubmissions = await _unitOfWork.MusicSubmissions.GetMusicSubmissionsByStatus(request.QueryParameters, request.Status, cancellationToken);
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
