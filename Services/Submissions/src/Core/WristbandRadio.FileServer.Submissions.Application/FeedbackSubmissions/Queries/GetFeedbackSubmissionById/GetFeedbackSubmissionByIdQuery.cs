namespace WristbandRadio.FileServer.Submissions.Application.FeedbackSubmissions.Queries.GetFeedbackSubmissionById;

public sealed record GetFeedbackSubmissionByIdQuery(string Id) : IRequest<FeedbackSubmission?>;
