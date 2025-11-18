namespace WristbandRadio.FileServer.Catalogue.Domain.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class InvalidArtistException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => Message;

    public InvalidArtistException()
    {
    }
    public InvalidArtistException(string message)
        : base(message)
    {
    }
    public InvalidArtistException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
