namespace WristbandRadio.FileServer.Common.Domain.Contracts.Specification;

public interface IValidator
{
    Task<bool> IsValid();
};
