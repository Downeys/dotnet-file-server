namespace WristbandRadio.FileServer.Submissions.Infrastructure.UnitOfWork;

public class UnitOfWorkImpl : IUnitOfWork
{
    private bool _disposed;
    private readonly DapperDataContext _dapperDataContext;
    public IMusicSubmissionRepository MusicSubmissions { get; private set; }
    public IImageLinkRepository ImageLinks { get; private set; }
    public IAudioLinkRepository AudioLinks { get; private set; }
    public UnitOfWorkImpl(DapperDataContext dapperDataContext)
    {
        _dapperDataContext = dapperDataContext;
        Init();
    }

    private void Init()
    {
        MusicSubmissions = new MusicSubmissionRepository(_dapperDataContext);
        ImageLinks = new ImageLinkRepository(_dapperDataContext);
        AudioLinks = new AudioLinkRepository(_dapperDataContext);
    }

    public async Task BeginTransaction()
    {
        var connection = await _dapperDataContext.GetConnection();
        if(connection.State != System.Data.ConnectionState.Open) connection.Open();
        _dapperDataContext.Transaction = connection.BeginTransaction();
    }

    public void Commit()
    {
        _dapperDataContext.Transaction?.Commit();
        _dapperDataContext.Transaction?.Dispose();
        _dapperDataContext.Transaction = null;
    }

    public async Task CommitAndCloseConnection()
    {
        var connection = await _dapperDataContext.GetConnection();
        _dapperDataContext.Transaction?.Commit();
        _dapperDataContext.Transaction?.Dispose();
        _dapperDataContext.Transaction = null;
        connection.Close();
        connection.Dispose();
    }

    public void Rollback()
    {
        _dapperDataContext.Transaction?.Rollback();
        _dapperDataContext.Transaction?.Dispose();
        _dapperDataContext.Transaction = null;
    }

    protected virtual async Task Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                var connection = await _dapperDataContext.GetConnection();
                _dapperDataContext.Transaction?.Dispose();
                connection.Dispose();
            }

            _disposed = true;
        }

    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
