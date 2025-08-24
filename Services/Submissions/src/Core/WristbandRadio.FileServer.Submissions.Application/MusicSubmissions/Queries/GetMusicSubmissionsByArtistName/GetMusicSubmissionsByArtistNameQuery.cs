﻿namespace WristbandRadio.FileServer.Submissions.Application.MusicSubmissions.Queries.GetMusicSubmissionsByArtistName;

public record GetMusicSubmissionsByArtistNameQuery(QueryParameters QueryParameters, string ArtistName) : IRequest<PageList<MusicSubmissionResponseDto>>;
