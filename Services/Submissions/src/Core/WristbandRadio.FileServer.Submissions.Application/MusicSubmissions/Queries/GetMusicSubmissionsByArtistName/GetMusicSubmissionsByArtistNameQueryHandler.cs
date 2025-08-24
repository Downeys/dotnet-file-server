namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Queries.GetMusicSubmissionsByArtistName;

public class GetMusicSubmissionsByArtistNameQueryHandler : IRequestHandler<GetMusicSubmissionsByArtistNameQuery, PageList<MusicSubmissionResponseDto>>
{
    private readonly ILogger<GetMusicSubmissionsByArtistNameQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetMusicSubmissionsByArtistNameQueryHandler(ILogger<GetMusicSubmissionsByArtistNameQueryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<PageList<MusicSubmissionResponseDto>> Handle(GetMusicSubmissionsByArtistNameQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching music submissions by artist name");
        var musicSubmissions = await _unitOfWork.MusicSubmissions.GetMusicSubmissionsByArtistName(request.QueryParameters, request.ArtistName, cancellationToken);
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
