namespace WristbandRadio.FileServer.Submissions.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    void RollBackAsync();
    void CommitAsync();
    void Dispose();
}
