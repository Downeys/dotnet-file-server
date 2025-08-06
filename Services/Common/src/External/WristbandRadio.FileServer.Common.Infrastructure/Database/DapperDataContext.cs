namespace WristbandRadio.FileServer.Common.Infrastructure.Database;

public sealed class DapperDataContext
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private IDbConnection? _connection;
    private IDbTransaction? _transaction;
    public DapperDataContext(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    public async Task<IDbConnection>? GetConnection(CancellationToken token = default)
    {
        if (_connection is null || _connection.State != ConnectionState.Open)
            _connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        return _connection;
    }

    public IDbTransaction? Transaction
    {
        get
        {
            return _transaction;
        }

        set
        {
            _transaction = value;
        }
    }
}
