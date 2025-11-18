namespace WristbandRadio.FileServer.Catalogue.Domain.Entities.Songs.Specifications;

public class IsNameLongEnough : ISpecification<Song>
{
    public bool IsSatisfiedBy(Song entity)
    {
        return entity.SongName.Length >= 2;
    }
}
