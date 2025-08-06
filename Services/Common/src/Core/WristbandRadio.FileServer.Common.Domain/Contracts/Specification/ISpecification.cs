namespace WristbandRadio.FileServer.Common.Domain.Contracts.Specification;
public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T entity);
};
