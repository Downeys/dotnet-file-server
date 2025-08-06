namespace WristbandRadio.FileServer.Submissions.Application.Models;

[ExcludeFromCodeCoverage]
public sealed record SubmissionInputDto(
        string ArtistName,
        string ContactName,
        string ContactEmail,
        string ContactPhone,
        bool OwnsRights
    );
