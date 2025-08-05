namespace WristbandRadio.FileServer.Submissions.Domain.Contracts;

public interface IUnitOfWork : IDisposable
{
    public IMusicSubmissionRepository MusicSubmissions { get; }
    public void BeginTransaction();
    public void Commit();
    public void CommitAndCloseConnection();
    public void Rollback();
}
