namespace WristbandRadio.FileServer.Common.Domain.Entities;

public interface IRepository<T> where T : IAggregateRoot
{
    Task<T> Get(string id);
    Task<IEnumerable<T>> GetAll();
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<bool> Delete(string id);
}
