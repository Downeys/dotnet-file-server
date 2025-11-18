namespace WristbandRadio.FileServer.Catalogue.Domain.Entities.Songs.Validators;

public class SongValidator : IValidator
{
    private readonly Song _song;
    public SongValidator(Song song)
    {
        _song = Guard.Against.Null(song);
    }
    private Task<bool> ValidateSongNameIsLongEnough()
    {
        var rule = new Specifications.IsNameLongEnough();
        var isOk = rule.IsSatisfiedBy(_song);
        if (!isOk) throw new InvalidSongException("Song name is too short.");
        return Task.FromResult(isOk);
    }
    private Task<bool> ValidateSongNameIsShortEnough()
    {
        var rule = new Specifications.IsNameShortEnough();
        var isOk = rule.IsSatisfiedBy(_song);
        if (!isOk) throw new InvalidSongException("Song name is too long.");
        return Task.FromResult(isOk);
    }
    public async Task<bool> IsValid()
    {
        var tasks = new List<Task<bool>>
        {
            ValidateSongNameIsLongEnough(),
            ValidateSongNameIsShortEnough()
        };
        var result = await Task.WhenAll(tasks);
        return result.All(x => x);
    }
}
