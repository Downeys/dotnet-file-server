namespace WristbandRadio.FileServer.Catalogue.Domain.Entities.Artists;

public class Artist : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string HometownZipCode { get; private set; }
    public string CurrentZipCode { get; private set; }
    public List<Album>? Albums { get; private set; }
    public Guid CreatedBy { get; private set; }

    private Artist(string name, string hometownZipcode, string currentZipcode, Guid createdBy,  Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = Guard.Against.NullOrEmpty(name);
        HometownZipCode = Guard.Against.NullOrEmpty(hometownZipcode);
        CurrentZipCode = Guard.Against.NullOrEmpty(currentZipcode);
        CreatedBy = Guard.Against.NullOrEmpty(createdBy);
    }

    public static Artist Create(string name, string hometownZipcode, string currentZipcode, Guid createdBy, Guid? id = null)
    {
        return new Artist(name, hometownZipcode, currentZipcode, createdBy, id);
    }

    public void AddAlbums(IEnumerable<Album> albums)
    {
        if (Albums == null)
        {
            Albums = new List<Album>();
        }
        Albums.AddRange(albums);
    }

    public ArtistDto ToDto()
    {
        return new ArtistDto
        {
            Id = Id,
            ArtistName = Name,
            HometownZipCode = HometownZipCode,
            CurrentZipCode = CurrentZipCode,
            CreatedBy = CreatedBy,
            CreatedDatetime = DateTime.UtcNow
        };
    }

    public ArtistResponseDto ToResponseDto()
    {
        return new ArtistResponseDto(Id, Name, HometownZipCode, CurrentZipCode);
    }

    public async Task<bool> IsValid()
    {
        var validator = new ArtistValidator(this);
        return validator.IsValid().Result;
    }
}
