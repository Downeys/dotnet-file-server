namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Commands.UpdateFeedbackSubmission;
public sealed record UpdateFeedbackSubmissionStatusCommand(Guid SubmissionId, string Status) : IRequest<bool>;
