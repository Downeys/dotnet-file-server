namespace WristbandRadio.FileServer.Common.Domain.Contracts.Persistance;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
}
