﻿namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Queries.GetMusicSubmissions;

public record GetMusicSubmissionsQuery(SubmissionQueryParameters QueryParameters) : IRequest<PageList<MusicSubmissionResponseDto>>;
