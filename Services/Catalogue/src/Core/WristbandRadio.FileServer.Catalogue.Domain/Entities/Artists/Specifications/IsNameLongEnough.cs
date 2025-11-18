namespace WristbandRadio.FileServer.Catalogue.Domain.Entities.Artists.Specifications;

public class IsNameLongEnough : ISpecification<Artist>
{
    public bool IsSatisfiedBy(Artist entity)
    {
        return entity.Name.Length >= 2;
    }
}
