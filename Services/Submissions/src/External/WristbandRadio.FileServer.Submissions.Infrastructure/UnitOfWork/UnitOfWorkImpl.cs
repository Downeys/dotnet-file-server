using Npgsql;

namespace WristbandRadio.FileServer.Submissions.Infrastructure.UnitOfWork;

public class UnitOfWorkImpl : IUnitOfWork, IDisposable
{
    private readonly NpgsqlTransaction _transaction;
    public NpgsqlConnection Connection { get; }
    public bool IsDisposed { get; private set; } = false;

    public UnitOfWorkImpl(string connectionString)
    {
        Connection = new NpgsqlConnection(connectionString);
        _transaction = Connection.BeginTransaction();
    }

    public async void RollBackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async void CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    public void Dispose()
    {
        _transaction.Dispose();
        Connection.Dispose();

        IsDisposed = true;
    }
}
