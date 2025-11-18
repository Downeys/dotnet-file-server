namespace WristbandRadio.FileServer.Catalogue.Domain.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class InvalidSongException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => Message;

    public InvalidSongException()
    {
    }
    public InvalidSongException(string message)
        : base(message)
    {
    }
    public InvalidSongException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
