namespace WristbandRadio.FileServer.Catalogue.Domain.Entities.Albums.Specifications;

public class IsNameLongEnough : ISpecification<Album>
{
    public bool IsSatisfiedBy(Album entity)
    {
        return entity.Name.Length >= 2;
    }
}
