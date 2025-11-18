namespace WristbandRadio.FileServer.Catalogue.Domain.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class InvalidAlbumException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => Message;

    public InvalidAlbumException()
    {
    }
    public InvalidAlbumException(string message)
        : base(message)
    {
    }
    public InvalidAlbumException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
