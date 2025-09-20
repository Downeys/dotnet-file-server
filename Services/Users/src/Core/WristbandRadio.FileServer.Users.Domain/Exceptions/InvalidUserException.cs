namespace WristbandRadio.FileServer.Users.Domain.Exceptions;
[Serializable]
[ExcludeFromCodeCoverage]
public class InvalidUserException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => Message;

    public InvalidUserException()
    {
    }
    public InvalidUserException(string message)
        : base(message)
    {
    }
    public InvalidUserException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
