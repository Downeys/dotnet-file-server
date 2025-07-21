namespace WristbandRadio.FileServer.Common.Domain.Specification;
public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T entity);
};
