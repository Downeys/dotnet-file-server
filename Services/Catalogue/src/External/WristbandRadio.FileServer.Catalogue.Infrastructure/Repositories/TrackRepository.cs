namespace WristbandRadio.FileServer.Catalogue.Infrastructure.Repositories;

public class TrackRepository : GenericRepository<TrackDto>, ITrackRepository
{
    public TrackRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }
}
