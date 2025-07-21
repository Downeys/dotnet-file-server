using Ardalis.GuardClauses;
using System.Data;
using WristbandRadio.FileServer.Submissions.Infrastructure.Settings;
using WristbandRadio.FileServer.Submissions.Infrastructure.UnitOfWork;

namespace WristbandRadio.FileServer.Submissions.Infrastructure.Contexts;

class PgContext : IDbContext
{
    private readonly string _connectionString;
    private UnitOfWorkImpl? _unitOfWork;
    private bool IsUnitOfWorkOpen => !(_unitOfWork == null || _unitOfWork.IsDisposed);

    public PgContext(DbSettings dbSettings)
    {
        _connectionString = Guard.Against.NullOrEmpty(dbSettings.ConnectionString);
    }

    public IUnitOfWork Create()
    {
        if (IsUnitOfWorkOpen)
        {
            throw new InvalidOperationException(
                "Cannot begin a transaction before the unit of work from the last one is disposed");
        }

        _unitOfWork = new UnitOfWorkImpl(_connectionString);
        return _unitOfWork;
    }

    public IDbConnection GetConnection()
    {
        if (!IsUnitOfWorkOpen)
        {
            throw new InvalidOperationException(
                "There is not current unit of work from which to get a connection. Call Create first");
        }

        return _unitOfWork!.Connection;
    }
}
