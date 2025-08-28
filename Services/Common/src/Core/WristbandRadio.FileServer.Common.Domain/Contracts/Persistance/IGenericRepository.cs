namespace WristbandRadio.FileServer.Common.Domain.Contracts.Persistance;

public interface IGenericRepository<T> where T : IDbEntity
{
    public Task<IEnumerable<T>> GetAsync(QueryParameters queryParameters, params string[] selectData);
    public Task<T> GetByIdAsync(Guid id, params string[] selectData);
    public Task<IEnumerable<T>> GetBySpecificColumnAsync(QueryParameters queryParameters, string columnName, string columnValue, params string[] selectData);
    public Task<Guid> AddAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task SoftDeleteAsync(Guid id, bool softDeleteFromRelatedChildTables = false);
    public Task<int> GetTotalCountAsync();
    public Task<bool> IsExistingAsync(string distinguishingUniqueKeyValue);
}
