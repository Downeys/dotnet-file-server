namespace WristbandRadio.FileServer.Submissions.Domain.Models.Presentation;

public record MusicSubmissionResponseDto(
        Guid Id,
        string ArtistName,
        string ContactName,
        string ContactEmail,
        string ContactPhone,
        bool OwnsRights
    );
