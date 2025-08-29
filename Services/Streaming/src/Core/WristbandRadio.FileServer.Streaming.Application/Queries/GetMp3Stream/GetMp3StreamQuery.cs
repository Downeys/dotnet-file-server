namespace WristbandRadio.FileServer.Streaming.Application.Queries.GetMp3Stream;

public sealed record GetMp3StreamQuery(Guid SongId) : IRequest<Stream>;
