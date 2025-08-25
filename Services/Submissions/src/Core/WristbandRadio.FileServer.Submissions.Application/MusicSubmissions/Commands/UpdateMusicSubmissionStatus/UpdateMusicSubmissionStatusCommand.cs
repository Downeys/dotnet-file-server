namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Commands.UpdateMusicSubmissionStatus;

public sealed record UpdateMusicSubmissionStatusCommand(Guid SubmissionId, string Status) : IRequest<bool>;
