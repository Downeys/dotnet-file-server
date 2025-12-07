namespace WristbandRadio.FileServer.Catalogue.Application.Songs.Queries.GetPaginatedSongsByArtistAlbumAndSongNames;

public sealed record GetPaginatedSongsByArtistAlbumAndSongNamesQuery(SongQueryParameters QueryParameters, string ArtistName, string AlbumName, string SongName) : IRequest<PageList<SongResponseDto>>;
