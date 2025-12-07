namespace WristbandRadio.FileServer.Catalogue.Domain.Utilities;

public class SongQueryParameters : QueryParameters
{
    public string? SongName { get; set; }
    public string? AlbumName { get; set; }
    public string? ArtistName { get; set; }
}
