namespace WristbandRadio.FileServer.Submissions.Domain.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class InvalidSubmissionException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => Message;

    public InvalidSubmissionException()
    {
    }
    public InvalidSubmissionException(string message)
        : base(message)
    {
    }
    public InvalidSubmissionException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
