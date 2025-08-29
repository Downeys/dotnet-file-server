namespace WristbandRadio.FileServer.Common.Domain.Contracts.Persistence;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
}
