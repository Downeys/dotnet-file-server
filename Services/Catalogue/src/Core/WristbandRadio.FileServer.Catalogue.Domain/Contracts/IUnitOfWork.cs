namespace WristbandRadio.FileServer.Catalogue.Domain.Contracts;

public interface IUnitOfWork : IDisposable
{
    public ITrackRepository Tracks { get; }
    public IArtistRepository Artists { get; }
    public IAlbumRepository Albums { get; }
    public ISongRepository Songs { get; }
    public Task BeginTransaction();
    public void Commit();
    public Task CommitAndCloseConnection();
    public void Rollback();
}