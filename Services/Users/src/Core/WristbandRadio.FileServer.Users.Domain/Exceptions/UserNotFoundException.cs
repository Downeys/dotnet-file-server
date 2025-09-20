namespace WristbandRadio.FileServer.Users.Domain.Exceptions;
[Serializable]
[ExcludeFromCodeCoverage]
public class UserNotFoundException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => Message;

    public UserNotFoundException()
    {
    }
    public UserNotFoundException(string message)
        : base(message)
    {
    }
    public UserNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
