namespace WristbandRadio.FileServer.Catalogue.Domain.Entities.Artists.Specifications;

public class IsCurrentZipCodeValid : ISpecification<Artist>
{
    public bool IsSatisfiedBy(Artist entity)
    {
        var zipCodeRegex = @"^\d{5}(-\d{4})?$"; // US ZIP code format
        return Regex.IsMatch(entity.CurrentZipCode, zipCodeRegex);
    }
}
