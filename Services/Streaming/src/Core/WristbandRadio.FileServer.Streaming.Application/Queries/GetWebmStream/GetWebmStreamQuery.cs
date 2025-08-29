namespace WristbandRadio.FileServer.Streaming.Application.Queries.GetWebmStream;

public sealed record GetWebmStreamQuery(Guid SongId) : IRequest<Stream>;
