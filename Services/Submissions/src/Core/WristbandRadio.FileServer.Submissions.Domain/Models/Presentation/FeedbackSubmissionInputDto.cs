namespace WristbandRadio.FileServer.Submissions.Domain.Models.Presentation;

public sealed record FeedbackSubmissionInputDto(string ContactName, string ContactEmail, string ContactPhone, string FeedbackType, string FeedbackText);
