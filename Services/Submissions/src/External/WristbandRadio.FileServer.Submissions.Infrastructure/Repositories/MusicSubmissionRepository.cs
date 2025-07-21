
using WristbandRadio.FileServer.Submissions.Infrastructure.UnitOfWork;

namespace WristbandRadio.FileServer.Submissions.Infrastructure.Repositories;

public class MusicSubmissionRepository : IMusicSubmissionRepository
{
    private readonly IUnitOfWork _unitOfWork;
    public Task<MusicSubmission> Add(MusicSubmission entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<MusicSubmission> Get(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MusicSubmission>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<MusicSubmission> Update(MusicSubmission entity)
    {
        throw new NotImplementedException();
    }
}
