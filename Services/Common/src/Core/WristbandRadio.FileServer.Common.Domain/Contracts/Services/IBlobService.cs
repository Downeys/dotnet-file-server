namespace WristbandRadio.FileServer.Common.Domain.Contracts.Services;

public interface IBlobService
{
    public Task<BlobResource> UploadFileAsync(string containerName, string fileName, Stream fileStream, CancellationToken token = default);
    public Task<Stream> DownloadFileAsync(string containerName, string fileName, CancellationToken token = default);
}
