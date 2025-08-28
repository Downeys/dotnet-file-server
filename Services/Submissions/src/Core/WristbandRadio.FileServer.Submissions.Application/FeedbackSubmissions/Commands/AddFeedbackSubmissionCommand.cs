namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Commands;

public sealed record AddFeedbackSubmissionCommand(string ContactName, string ContactEmail, string ContactPhone, string FeedbackType, string FeedbackText): IRequest<Guid>;
