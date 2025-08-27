namespace WristbandRadio.FileServer.Submissions.Infrastructure.Services;

public class MusicSubmissionBlobService : IMusicSubmissionBlobService
{
    private readonly IBlobService _blobService;
    private readonly string _imageContanerName;
    private readonly string _audioContainerName;

    public MusicSubmissionBlobService(IBlobService blobService)
    {
        var imageContainerName = Environment.GetEnvironmentVariable("IMAGE_SUBMISSION_CONTAINER");
        var audioContainerName = Environment.GetEnvironmentVariable("AUDIO_SUBMISSION_CONTAINER");
        _blobService = Guard.Against.Null(blobService);
        _imageContanerName = Guard.Against.NullOrEmpty(imageContainerName);
        _audioContainerName = Guard.Against.NullOrEmpty(audioContainerName);
    }

    public async Task<BlobResource> UploadPhotoSubmission(Stream fileStream, string fileName, CancellationToken token = default)
    {
        return await _blobService.UploadFileAsync(_imageContanerName, fileName, fileStream, token);
    }

    public async Task<BlobResource> UploadSongSubmission(Stream fileStream, string fileName, CancellationToken token = default)
    {
        return await _blobService.UploadFileAsync(_audioContainerName, fileName, fileStream, token);
    }
}
