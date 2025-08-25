namespace WristbandRadio.FileServer.Submissions.Domain.Entities;

public sealed class MusicSubmission : Entity, IAggregateRoot
{
    public Guid Id { get; private set; }
    public string ArtistName { get; private set; }
    public string ContactName { get; private set; }
    public string ContactEmail { get; private set; }
    public string ContactPhone { get; private set; }
    public bool OwnsRights { get; private set; }
    public DateTime? CreatedDatetime { get; private set; }
    public Guid CreatedBy { get; private set; }
    public string Status { get; private set; }

    private MusicSubmission(string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights, Guid createdBy, string? status)
    {
        Id = Guid.NewGuid();
        ArtistName = Guard.Against.NullOrEmpty(artistName);
        ContactName = Guard.Against.NullOrEmpty(contactName);
        ContactEmail = Guard.Against.NullOrEmpty(contactEmail);
        ContactPhone = Guard.Against.NullOrEmpty(contactPhone);
        OwnsRights = ownsRights;
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
        CreatedDatetime = DateTime.UtcNow;
        Status = Guard.Against.NullOrEmpty(status);
    }

    private MusicSubmission(Guid? id, string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights, Guid createdBy, string? status)
    {
        Id = Guard.Against.NullOrEmpty(id);
        ArtistName = Guard.Against.NullOrEmpty(artistName);
        ContactName = Guard.Against.NullOrEmpty(contactName);
        ContactEmail = Guard.Against.NullOrEmpty(contactEmail);
        ContactPhone = Guard.Against.NullOrEmpty(contactPhone);
        OwnsRights = ownsRights;
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
        CreatedDatetime = DateTime.UtcNow;
        Status = Guard.Against.NullOrEmpty(status);
    }

    public static MusicSubmission Create(string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights, Guid createdBy, Guid? id = null, string? status = "Pending")
    {
        return id != null
            ? new MusicSubmission(id, artistName, contactName, contactEmail, contactPhone, ownsRights, createdBy, status)
            : new MusicSubmission(artistName, contactName, contactEmail, contactPhone, ownsRights, createdBy, status);
    }

    public bool IsValid()
    {
        var validator = new MusicSubmissionValidator(this);
        return validator.IsValid().Result;
    }

    public void UpdateStatus(string status)
    {
        Status = status;
    }

    public MusicSubmissionDto ToDto() => new MusicSubmissionDto
    {
        Id = Id,
        ArtistName = ArtistName,
        ContactName = ContactName,
        ContactEmail = ContactEmail,
        ContactPhone = ContactPhone,
        OwnsRights = OwnsRights,
        CreatedBy = CreatedBy,
        CreatedDatetime = CreatedDatetime,
        Status = Status
    };

    public MusicSubmissionResponseDto ToResponseDto()
    {
        return new MusicSubmissionResponseDto
            (
                Id,
                ArtistName,
                ContactName,
                ContactEmail,
                ContactPhone,
                OwnsRights,
                Status
            );
    }
};

