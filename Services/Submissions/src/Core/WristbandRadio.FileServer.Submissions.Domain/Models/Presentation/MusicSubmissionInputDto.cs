namespace WristbandRadio.FileServer.Submissions.Domain.Models.Presentation;

[ExcludeFromCodeCoverage]
public record MusicSubmissionInputDto(
        string ArtistName,
        string ContactName,
        string ContactEmail,
        string ContactPhone,
        bool OwnsRights,
        List<IFormFile> Files
    );
