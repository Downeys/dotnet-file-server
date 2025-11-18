namespace WristbandRadio.FileServer.Catalogue.Infrastructure.Repositories;

public class SongRepository : GenericRepository<SongDto>, ISongRepository
{
    public SongRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }
}
