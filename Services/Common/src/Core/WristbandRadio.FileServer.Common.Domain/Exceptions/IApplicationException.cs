namespace WristbandRadio.FileServer.Common.Domain.Exceptions;

public interface IApplicationException
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}