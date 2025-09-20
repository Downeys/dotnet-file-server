namespace WristbandRadio.FileServer.Users.Domain.Contracts;

public interface IUnitOfWork
{
    public IUsersRepository Users { get; }
    public Task BeginTransaction();
    public void Commit();
    public Task CommitAndCloseConnection();
    public void Rollback();
}
