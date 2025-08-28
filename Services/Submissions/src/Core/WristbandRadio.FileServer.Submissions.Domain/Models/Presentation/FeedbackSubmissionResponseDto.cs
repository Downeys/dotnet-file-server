namespace WristbandRadio.FileServer.Submissions.Domain.Models.Presentation;

public sealed record FeedbackSubmissionResponseDto
    (
        Guid id,
        Guid createdBy,
        string contactName,
        string contactEmail,
        string contactPhone,
        string submissionType,
        string feedbackText,
        string status
    );
