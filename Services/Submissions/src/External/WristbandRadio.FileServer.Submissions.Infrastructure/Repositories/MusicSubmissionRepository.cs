
namespace WristbandRadio.FileServer.Submissions.Infrastructure.Repositories;

public sealed class MusicSubmissionRepository : GenericRepository<MusicSubmissionDto>, IMusicSubmissionRepository
{
    public MusicSubmissionRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }
}
