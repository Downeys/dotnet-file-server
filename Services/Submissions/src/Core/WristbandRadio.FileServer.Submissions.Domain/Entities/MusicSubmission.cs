namespace WristbandRadio.FileServer.Submissions.Domain.Entities;

public sealed class MusicSubmission : Entity, IAggregateRoot
{
    public Guid Id { get; set; }
    public string ArtistName { get; set; }
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }
    public bool OwnsRights { get; set; }
    public DateTime? CreatedDatetime { get; set; }
    public Guid CreatedBy { get; set; }

    private MusicSubmission(string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights, Guid createdBy)
    {
        Id = Guid.NewGuid();
        ArtistName = Guard.Against.NullOrEmpty(artistName);
        ContactName = Guard.Against.NullOrEmpty(contactName);
        ContactEmail = Guard.Against.NullOrEmpty(contactEmail);
        ContactPhone = Guard.Against.NullOrEmpty(contactPhone);
        OwnsRights = ownsRights;
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
        CreatedDatetime = DateTime.UtcNow;
    }

    private MusicSubmission(Guid? id, string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights, Guid createdBy)
    {
        Id = Guard.Against.NullOrEmpty(id);
        ArtistName = Guard.Against.NullOrEmpty(artistName);
        ContactName = Guard.Against.NullOrEmpty(contactName);
        ContactEmail = Guard.Against.NullOrEmpty(contactEmail);
        ContactPhone = Guard.Against.NullOrEmpty(contactPhone);
        OwnsRights = ownsRights;
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
        CreatedDatetime = DateTime.UtcNow;
    }

    public static MusicSubmission Create(string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights, Guid createdBy, Guid? id = null)
    {
        return id != null
            ? new MusicSubmission(id, artistName, contactName, contactEmail, contactPhone, ownsRights, createdBy)
            : new MusicSubmission(artistName, contactName, contactEmail, contactPhone, ownsRights, createdBy);
    }
};

