namespace WristbandRadio.FileServer.Catalogue.Domain.Entities.Albums;

public class Album : Entity
{
    public string Name { get; private set; }
    public string ArtUrl { get; private set; }
    public Guid ArtistId { get; private set; }
    public List<Track>? Tracks { get; private set; }
    public string PurchaseUrl { get; private set; }

    public Guid CreatedBy { get; private set; }

    private Album(Guid artistId, string albumName, string artUrl, string purchaseUrl, Guid createdBy,  Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        ArtistId = artistId;
        Name = albumName;
        ArtUrl = artUrl;
        PurchaseUrl = purchaseUrl;
        CreatedBy = createdBy;
    }

    public static Album Create(Guid artistId, string albumName, string artUrl, string purchaseUrl, Guid createdBy, Guid? id = null)
    {
        return new Album(artistId, albumName, artUrl, purchaseUrl, createdBy, id);
    }

    public AlbumDto ToDto()
    {
        return new AlbumDto
        {
            Id = Id,
            ArtistId = ArtistId,
            AlbumName = Name,
            AlbumArtUrl = ArtUrl,
            AlbumPurchaseUrl = PurchaseUrl,
            CreatedBy= CreatedBy,
            CreatedDatetime = DateTime.UtcNow
        };
    }

    public AlbumResponseDto ToResponseDto()
    {
        return new AlbumResponseDto(Id, ArtistId, Name, ArtUrl, PurchaseUrl);
    }

    public async Task<bool> IsValid()
    {
        var validator = new AlbumValidator(this);
        return await validator.IsValid();
    }
}
