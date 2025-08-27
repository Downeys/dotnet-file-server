namespace WristbandRadio.FileServer.Submissions.Infrastructure.Repositories;

public class AudioLinkRepository : GenericRepository<AudioLinkDto>, IAudioLinkRepository
{
    public AudioLinkRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }
}
