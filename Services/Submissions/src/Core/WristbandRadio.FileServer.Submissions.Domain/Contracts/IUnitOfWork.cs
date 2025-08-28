namespace WristbandRadio.FileServer.Submissions.Domain.Contracts;

public interface IUnitOfWork : IDisposable
{
    public IMusicSubmissionRepository MusicSubmissions { get; }
    public IImageLinkRepository ImageLinks { get; }
    public IAudioLinkRepository AudioLinks { get; }
    public IFeedbackSubmissionRepository FeedbackSubmissions { get; }
    public Task BeginTransaction();
    public void Commit();
    public Task CommitAndCloseConnection();
    public void Rollback();
}
