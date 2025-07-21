namespace WristbandRadio.FileServer.Common.Domain.Specification;

public interface IValidator
{
    Task<bool> IsValid();
};
