namespace WristbandRadio.FileServer.Common.Infrastructure.Database;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = Guard.Against.NullOrEmpty(connectionString);
    }

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(token);
        return connection;
    }
}
