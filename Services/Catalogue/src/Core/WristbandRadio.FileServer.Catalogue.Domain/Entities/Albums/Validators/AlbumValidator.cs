namespace WristbandRadio.FileServer.Catalogue.Domain.Entities.Albums.Validators;

public class AlbumValidator : IValidator
{
    private readonly Album _album;
    public AlbumValidator(Album album)
    {
        _album = Guard.Against.Null(album);
    }
    private Task<bool> ValidateAlbumNameIsLongEnough()
    {
        var rule = new Specifications.IsNameLongEnough();
        var isOk = rule.IsSatisfiedBy(_album);
        if (!isOk) throw new InvalidAlbumException("Album name is too short.");
        return Task.FromResult(isOk);
    }
    private Task<bool> ValidateAlbumNameIsShortEnough()
    {
        var rule = new Specifications.IsNameShortEnough();
        var isOk = rule.IsSatisfiedBy(_album);
        if (!isOk) throw new InvalidAlbumException("Album name is too long.");
        return Task.FromResult(isOk);
    }
    public async Task<bool> IsValid()
    {
        var tasks = new List<Task<bool>>
        {
            ValidateAlbumNameIsLongEnough(),
            ValidateAlbumNameIsShortEnough()
        };
        var result = await Task.WhenAll(tasks);
        return result.All(x => x);
    }
}
