using System.Data;
using WristbandRadio.FileServer.Submissions.Infrastructure.UnitOfWork;

namespace WristbandRadio.FileServer.Submissions.Infrastructure.Contexts;

public interface IDbContext
{
    IUnitOfWork Create();
    IDbConnection GetConnection();
}

