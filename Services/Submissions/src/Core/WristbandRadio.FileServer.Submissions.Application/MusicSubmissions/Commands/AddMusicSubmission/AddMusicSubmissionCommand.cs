namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Commands.AddMusicSubmission;

public record AddMusicSubmissionCommand(
        string ArtistName,
        string ContactName,
        string ContactEmail,
        string ContactPhone,
        bool OwnsRights
    ) : IRequest<Guid>;

