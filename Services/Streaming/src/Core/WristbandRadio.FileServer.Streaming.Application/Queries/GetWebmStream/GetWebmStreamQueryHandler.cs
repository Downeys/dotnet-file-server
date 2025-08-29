namespace WristbandRadio.FileServer.Streaming.Application.Queries.GetWebmStream;

public sealed class GetWebmStreamQueryHandler : IRequestHandler<GetWebmStreamQuery, Stream>
{
    private readonly ILogger<GetWebmStreamQueryHandler> _logger;
    private readonly IBlobService _blobService;
    private readonly string _webmContainerName;
    public GetWebmStreamQueryHandler(ILogger<GetWebmStreamQueryHandler> logger, IBlobService blobService)
    {
        var containerName = Environment.GetEnvironmentVariable("WEBM_CONTAINER");

        _logger = Guard.Against.Null(logger);
        _blobService = Guard.Against.Null(blobService);
        _webmContainerName = Guard.Against.NullOrWhiteSpace(containerName);
    }
    public async Task<Stream> Handle(GetWebmStreamQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("fetching webm stream for song id {SongId}", request.SongId);
        var fileName = $"{request.SongId}.webm";
        return await _blobService.DownloadFileAsync(_webmContainerName, fileName, cancellationToken);
    }
}
