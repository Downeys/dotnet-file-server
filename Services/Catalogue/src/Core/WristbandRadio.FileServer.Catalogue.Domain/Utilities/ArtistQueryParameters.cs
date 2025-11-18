namespace WristbandRadio.FileServer.Catalogue.Domain.Utilities;

public class ArtistQueryParameters : QueryParameters
{
    public string? ArtistName { get; set; }
    public string? HometownZipCode { get; set; }
}