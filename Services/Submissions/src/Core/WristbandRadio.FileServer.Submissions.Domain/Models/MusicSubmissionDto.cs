namespace WristbandRadio.FileServer.Submissions.Domain.Models;

[ExcludeFromCodeCoverage]
[TableName("music_submissions")]
public sealed class MusicSubmissionDto : IDbEntity
{
    [PrimaryKey]
    [ColumnName("id")]
    public string Id { get; set; }
    [ColumnName("artist_name")]
    public string ArtistName { get; set; }
    [ColumnName("contact_name")]
    public string ContactName { get; set; }
    [ColumnName("contact_email")]
    public string ContactEmail { get; set; }
    [ColumnName("contact_phone")]
    public string ContactPhone { get; set; }
    [ColumnName("owns_rights")]
    public bool OwnsRights { get; set; }
    [ColumnName("created_datetime")]
    public DateTime? CreatedDateTime { get; set; }
    [ColumnName("updated_datetime")]
    public DateTime? UpdatedDateTime { get; set; }
    [ColumnName("deleted_datetime")]
    public DateTime? DeletedDateTime { get; set; }
    [ColumnName("created_by")]
    public string CreatedBy { get; set; }
    [ColumnName("updated_by")]
    public string? UpdatedBy { get; set; }
    [ColumnName("deleted_by")]
    public string? DeletedBy { get; set; }

    public MusicSubmissionDto(string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights, string createdBy)
    {
        this.Id = Guid.NewGuid().ToString();
        ArtistName = Guard.Against.NullOrEmpty(artistName);
        ContactName = Guard.Against.NullOrEmpty(contactName);
        ContactEmail = Guard.Against.NullOrEmpty(contactEmail);
        ContactPhone = Guard.Against.NullOrEmpty(contactPhone);
        OwnsRights = ownsRights;
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
    }

    public MusicSubmissionDto(string Id, string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights, string createdBy)
    {
        this.Id = Guard.Against.NullOrEmpty(Id);
        ArtistName = Guard.Against.NullOrEmpty(artistName);
        ContactName = Guard.Against.NullOrEmpty(contactName);
        ContactEmail = Guard.Against.NullOrEmpty(contactEmail);
        ContactPhone = Guard.Against.NullOrEmpty(contactPhone);
        OwnsRights = ownsRights;
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
    }
}
