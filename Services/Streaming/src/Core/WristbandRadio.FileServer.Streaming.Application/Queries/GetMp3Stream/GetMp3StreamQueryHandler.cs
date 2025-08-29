namespace WristbandRadio.FileServer.Streaming.Application.Queries.GetMp3Stream;

public class GetMp3StreamQueryHandler : IRequestHandler<GetMp3StreamQuery, Stream>
{
    private readonly ILogger<GetMp3StreamQueryHandler> _logger;
    private readonly IBlobService _blobService;
    private readonly string _mp3ContainerName;
    public GetMp3StreamQueryHandler(ILogger<GetMp3StreamQueryHandler> logger, IBlobService blobService)
    {
        var containerName = Environment.GetEnvironmentVariable("MP3_CONTAINER");

        _logger = Guard.Against.Null(logger);
        _blobService = Guard.Against.Null(blobService);
        _mp3ContainerName = Guard.Against.NullOrWhiteSpace(containerName);
    }
    public async Task<Stream> Handle(GetMp3StreamQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching mp3 stream for song id {SongId}", request.SongId);
        var fileName = $"{request.SongId}.mp3";
        return await _blobService.DownloadFileAsync(_mp3ContainerName, fileName, cancellationToken);
    }
}
