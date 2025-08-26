namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Commands.RemoveMusicSubmission;

public sealed record RemoveMusicSubmissionCommand(Guid Id) : IRequest<bool>;
