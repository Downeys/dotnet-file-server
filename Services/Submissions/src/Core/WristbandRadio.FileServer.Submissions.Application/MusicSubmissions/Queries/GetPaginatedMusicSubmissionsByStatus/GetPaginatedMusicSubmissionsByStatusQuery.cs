﻿namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Queries.GetPaginatedMusicSubmissionsByStatus;

public sealed record GetPaginatedMusicSubmissionsByStatusQuery(QueryParameters QueryParameters, string Status) : IRequest<PageList<MusicSubmissionResponseDto>>;
