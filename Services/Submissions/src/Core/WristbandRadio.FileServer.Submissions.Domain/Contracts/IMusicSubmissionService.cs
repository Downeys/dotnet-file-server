namespace WristbandRadio.FileServer.Submissions.Domain.Contracts;

public interface IMusicSubmissionService
{
    public Task<Guid> PersistMusicSubmission(MusicSubmission musicSubmission);
}
