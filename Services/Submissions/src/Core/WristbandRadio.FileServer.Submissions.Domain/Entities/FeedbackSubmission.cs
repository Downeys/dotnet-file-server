namespace WristbandRadio.FileServer.Submissions.Domain.Entities;

public class FeedbackSubmission : Entity, IAggregateRoot
{
    public string ContactName { get; private set; }
    public string ContactEmail { get; private set; }
    public string ContactPhone { get; private set; }
    public SubmissionType SubmissionType { get; private set; }
    public string FeedbackText { get; private set; }
    public string Status { get; private set; }
    public Guid CreatedBy { get; private set; }
    private FeedbackSubmission(
        string contactName,
        string contactEmail,
        string contactPhone,
        SubmissionType submissionType,
        string feedbackText,
        Guid createdBy,
        string status = "New")
    {
        Id = Guid.NewGuid();
        ContactName = Guard.Against.NullOrEmpty(contactName);
        ContactEmail = Guard.Against.NullOrEmpty(contactEmail);
        ContactPhone = Guard.Against.NullOrEmpty(contactPhone);
        SubmissionType = Guard.Against.Null(submissionType);
        FeedbackText = Guard.Against.NullOrEmpty(feedbackText);
        Status = Guard.Against.NullOrEmpty(status);
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
    }
    private FeedbackSubmission(
        Guid? id,
        string contactName,
        string contactEmail,
        string contactPhone,
        SubmissionType submissionType,
        string feedbackText,
        Guid createdBy,
        string status = "New")
    {
        Id = Guard.Against.NullOrEmpty(id);
        ContactName = Guard.Against.NullOrEmpty(contactName);
        ContactEmail = Guard.Against.NullOrEmpty(contactEmail);
        ContactPhone = Guard.Against.NullOrEmpty(contactPhone);
        SubmissionType = Guard.Against.Null(submissionType);
        FeedbackText = Guard.Against.NullOrEmpty(feedbackText);
        Status = Guard.Against.NullOrEmpty(status);
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
    }
    public static FeedbackSubmission Create(
        string contactName,
        string contactEmail,
        string contactPhone,
        SubmissionType submissionType,
        string feedbackText,
        Guid createdBy,
        Guid? id = null,
        string status = "New")
    {
        // Add any necessary validation here
        return id != null
            ? new FeedbackSubmission(id, contactName, contactEmail, contactPhone, submissionType, feedbackText, createdBy, status)
            : new FeedbackSubmission(contactName, contactEmail, contactPhone, submissionType, feedbackText, createdBy, status);
    }

    public async Task<bool> IsValid()
    {
        var validator = new FeedbackSubmissionValidator(this);
        return await validator.IsValid();
    }

    public FeedbackSubmissionResponseDto ToResponseDto()
    {
        return new FeedbackSubmissionResponseDto(
            Id,
            CreatedBy,
            ContactName,
            ContactEmail,
            ContactPhone,
            SubmissionType.ToString(),
            FeedbackText,
            Status
        );
    }

    public FeedbackSubmissionDto ToDto()
    {
        return new FeedbackSubmissionDto
        {
            Id = this.Id,
            ContactName = this.ContactName,
            ContactEmail = this.ContactEmail,
            ContactPhone = this.ContactPhone,
            SubmissionTypeId = (int)this.SubmissionType,
            FeedbackText = this.FeedbackText,
            Status = this.Status,
            CreatedBy = this.CreatedBy,
            CreatedDatetime = DateTime.UtcNow
        };
    }
}
