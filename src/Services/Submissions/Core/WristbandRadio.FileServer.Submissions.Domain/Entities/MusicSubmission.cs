using Ardalis.GuardClauses;
using WristbandRadio.FileServer.Common.Domain.Entities;

namespace WristbandRadio.FileServer.Submissions.Domain.Entities;

public class MusicSubmission : AggregateRoot
{
    public string ArtistName { get; private set; }
    public string ContactName { get; private set; }
    public string ContactEmail { get; private set; }
    public string ContactPhone { get; private set; }
    public bool OwnsRights { get; private set; }
    public virtual List<string> ImageUrls { get; private set; } = [];
    public virtual List<string> AudioUrls { get; private set; } = [];

    private MusicSubmission(string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights, Guid id = default)
    {
        Id = id;
        ArtistName = Guard.Against.NullOrEmpty(artistName);
        ContactName = Guard.Against.NullOrEmpty(contactName);
        ContactEmail = Guard.Against.NullOrEmpty(contactEmail);
        ContactPhone = Guard.Against.NullOrEmpty(contactPhone);
        OwnsRights = ownsRights;
    }

    public static MusicSubmission Create(Guid id, string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights)
    {
        return new MusicSubmission(artistName, contactName, contactEmail, contactPhone, ownsRights, id);
    }

    public static MusicSubmission Create(string artistName, string contactName, string contactEmail, string contactPhone, bool ownsRights)
    {
        return new MusicSubmission(artistName, contactName, contactEmail, contactPhone, ownsRights);
    }

    public void AddImageUrls(List<string> imageUrls)
    {
        Guard.Against.NullOrEmpty(imageUrls);
        ImageUrls.AddRange(imageUrls);
    }

    public void AddAudioUrls(List<string> audioUrls)
    {
        Guard.Against.NullOrEmpty(audioUrls);
        AudioUrls.AddRange(audioUrls);
    }
};

