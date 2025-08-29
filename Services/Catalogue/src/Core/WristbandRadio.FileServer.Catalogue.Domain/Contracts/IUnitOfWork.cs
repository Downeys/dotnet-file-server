namespace WristbandRadio.FileServer.Catalogue.Domain.Contracts;

public interface IUnitOfWork : IDisposable
{
    public ITrackRepository TrackRepository { get; }
    public Task BeginTransaction();
    public void Commit();
    public Task CommitAndCloseConnection();
    public void Rollback();
}