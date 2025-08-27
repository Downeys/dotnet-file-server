namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Commands.AddMusicSubmission;

public class AddMusicSubmissionCommandHandler : IRequestHandler<AddMusicSubmissionCommand, Guid>
{
    private readonly ILogger<AddMusicSubmissionCommandHandler> _logger;
    private readonly IMusicSubmissionBlobService _blobService;
    private readonly IMusicSubmissionService _service;

    public AddMusicSubmissionCommandHandler(ILogger<AddMusicSubmissionCommandHandler> logger, IMusicSubmissionBlobService blobService, IMusicSubmissionService service)
    {
        _logger = Guard.Against.Null(logger);
        _blobService = Guard.Against.Null(blobService);
        _service = Guard.Against.Null(service);
    }

    public async Task<Guid> Handle(AddMusicSubmissionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling AddMusicSubmissionCommand.");

        var submissionEntity = ValidateRequest(request);

        var songs = UploadSongs(request.AudioFiles, cancellationToken);
        var images = UploadImages(request.ImageFiles, cancellationToken);
        submissionEntity.SetSongs(songs);
        submissionEntity.SetImages(images);

        var submissionId = await _service.PersistMusicSubmission(submissionEntity);
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

    private List<BlobResource> UploadSongs(IEnumerable<Stream> audioFiles, CancellationToken cancellationToken)
    {
        var songs = new List<BlobResource>();
        foreach (var file in audioFiles)
        {
            var fileName = Guid.NewGuid().ToString();
            var audioUrl = _blobService.UploadSongSubmission(file, fileName, cancellationToken).Result;
            songs.Add(audioUrl);
            _logger.LogInformation("Uploaded song to {SongUrl}", audioUrl);
        }
        return songs;
    }

    private List<BlobResource> UploadImages(IEnumerable<Stream> imageFiles, CancellationToken cancellationToken)
    {
        var images = new List<BlobResource>();
        foreach (var file in imageFiles)
        {
            var fileName = Guid.NewGuid().ToString();
            var imageUrl = _blobService.UploadPhotoSubmission(file, fileName, cancellationToken).Result;
            images.Add(imageUrl);
            _logger.LogInformation("Uploaded photo to {PhotoUrl}", imageUrl);
        }
        return images;
    }
}
