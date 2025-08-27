namespace WristbandRadio.FileServer.Submissions.Domain.Contracts;

public interface IMusicSubmissionBlobService
{
    public Task<BlobResource> UploadSongSubmission(Stream fileStream, string fileName, CancellationToken token = default);
    public Task<BlobResource> UploadPhotoSubmission(Stream fileStream, string fileName, CancellationToken token = default);
}
