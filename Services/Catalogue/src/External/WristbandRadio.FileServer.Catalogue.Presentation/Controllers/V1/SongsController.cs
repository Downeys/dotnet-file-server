namespace WristbandRadio.FileServer.Catalogue.Presentation.Controllers.V1;
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class SongsController : ControllerBase
{
    private readonly ILogger<SongsController> _logger;
    private readonly ISender _sender;
    public SongsController(ILogger<SongsController> logger, ISender sender)
    {
        _logger = Guard.Against.Null(logger);
        _sender = Guard.Against.Null(sender);
    }

    [HttpGet]
    public async Task<IActionResult> GetSongs([FromQuery] SongQueryParameters queryParameters)
    {
        _logger.LogInformation("Get songs method called");
        PageList<SongResponseDto> songs;
        var artistName = queryParameters.ArtistName;
        var albumName = queryParameters.AlbumName;
        var songName = queryParameters.SongName;

        if (artistName != null && albumName != null && songName != null)
            songs = await _sender.Send(new GetPaginatedSongsByArtistAlbumAndSongNamesQuery(queryParameters, artistName, albumName, songName));
        else
            songs = await _sender.Send(new GetPaginatedSongsQuery(queryParameters));
        if (songs is null)
        {
            return NotFound();
        }
        return Ok(songs);
    }

    [HttpGet("song/{id:length(36)}")]
    public async Task<IActionResult> GetSongsById(string id)
    {
        _logger.LogInformation("Get song by Id method called");
        var song = await _sender.Send(new GetSongByIdQuery(id));
        if (song is null)
        {
            return NotFound();
        }
        return Ok(song.ToResponseDto());
    }

    [HttpPost("song")]
    public async Task<IActionResult> AddSong([FromForm] SongInputDto songInput)
    {
        _logger.LogInformation("Post song method called");

        var addSongCommand = new AddSongCommand(songInput);
        var songId = await _sender.Send(addSongCommand);
        return CreatedAtAction("AddSong", new { id = songId });
    }

    [HttpPut("song/{id:length(36)}")]
    public async Task<IActionResult> UpdateSong(string id, SongInputDto songInput)
    {
        _logger.LogInformation("Update song method called");
        var updateSongCommand = new UpdateSongCommand
            (
                Guid.Parse(id),
                songInput
            );
        var result = await _sender.Send(updateSongCommand);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("song/{id:length(36)}")]
    public async Task<IActionResult> RemoveSong(string id)
    {
        _logger.LogInformation("Remove song method called");
        var removeSongCommand = new RemoveSongCommand(Guid.Parse(id));
        var result = await _sender.Send(removeSongCommand);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
