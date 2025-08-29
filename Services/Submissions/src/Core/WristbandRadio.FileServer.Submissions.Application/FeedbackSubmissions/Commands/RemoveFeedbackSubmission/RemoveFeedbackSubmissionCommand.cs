namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Commands.RemoveFeedbackSubmission;

public sealed record RemoveFeedbackSubmissionCommand(Guid Id) : IRequest<bool>;
