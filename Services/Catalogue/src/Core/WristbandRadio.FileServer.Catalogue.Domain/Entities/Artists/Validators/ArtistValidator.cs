namespace WristbandRadio.FileServer.Catalogue.Domain.Entities.Artists.Validators;

public class ArtistValidator : IValidator
{
    private readonly Artist _artist;
    public ArtistValidator(Artist artist)
    {
        _artist = Guard.Against.Null(artist);
    }
    private Task<bool> ValidateArtistNameIsLongEnough()
    {
        var rule = new IsNameShortEnough();
        var isOk = rule.IsSatisfiedBy(_artist);
        if (!isOk) throw new InvalidArtistException("Artist name is too short.");
        return Task.FromResult(isOk);
    }

    private Task<bool> ValidateArtistNameIsShortEnough()
    {
        var rule = new IsNameShortEnough();
        var isOk = rule.IsSatisfiedBy(_artist);
        if (!isOk) throw new InvalidArtistException("Artist name is too long.");
        return Task.FromResult(isOk);
    }

    private Task<bool> ValidateHometownZipCode()
    {
        var rule = new IsHometownZipCodeValid();
        var isOk = rule.IsSatisfiedBy(_artist);
        if (!isOk) throw new InvalidArtistException("Hometown zip code is invalid.");
        return Task.FromResult(isOk);
    }

    private Task<bool> ValidateCurrentZipCode()
    {
        var rule = new IsCurrentZipCodeValid();
        var isOk = rule.IsSatisfiedBy(_artist);
        if (!isOk) throw new InvalidArtistException("Artist name is too short.");
        return Task.FromResult(isOk);
    }


    public async Task<bool> IsValid()
    {
        var tasks = new List<Task<bool>>
        {
            ValidateArtistNameIsLongEnough(),
            ValidateArtistNameIsShortEnough(),
            ValidateHometownZipCode(),
            ValidateCurrentZipCode()
        };
        var result = await Task.WhenAll(tasks);
        return result.All(x => x);
    }
}
