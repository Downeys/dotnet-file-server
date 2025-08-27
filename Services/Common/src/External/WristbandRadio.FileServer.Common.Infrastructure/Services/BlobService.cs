namespace WristbandRadio.FileServer.Common.Infrastructure.Services;

public class BlobService : IBlobService
{
    private readonly ILogger<BlobService> _logger; 
    private readonly BlobServiceClient _blobServiceClient;
    public BlobService(ILogger<BlobService> logger)
    {
        var connectionString = Environment.GetEnvironmentVariable("BLOB_CONNECTION_STRING");
        _logger = logger;
        _blobServiceClient = new BlobServiceClient(connectionString);
    }
    public async Task<Stream> DownloadFileAsync(string containerName, string fileName, CancellationToken token = default)
    {
        _logger.LogInformation("fetching blob from storage account");
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        var downloadInfo = await blobClient.DownloadContentAsync(token);
        return downloadInfo.Value.Content.ToStream();
    }

    public async Task<BlobResource> UploadFileAsync(string containerName, string fileName, Stream fileStream, CancellationToken token = default)
    {
        _logger.LogInformation("uploading blob to storage account");
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, overwrite: true, cancellationToken: token);
        return new BlobResource(fileName, blobClient.Uri.ToString());
    }
}
