namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Queries.GetMusicSubmissionById;

public record GetMusicSubmissionByIdQuery(string id) : IRequest<MusicSubmission?>;
