namespace WristbandRadio.FileServer.Common.Domain.Models;

public class BlobResource
{
    public string BlobName { get; set; }
    public string BlobUrl { get; set; }

    public BlobResource(string blobName, string blobUrl)
    {
        BlobName = Guard.Against.NullOrEmpty(blobName);
        BlobUrl = Guard.Against.NullOrEmpty(blobUrl);
    }
}
