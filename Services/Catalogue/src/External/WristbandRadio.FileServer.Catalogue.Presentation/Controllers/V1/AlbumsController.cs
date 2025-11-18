namespace WristbandRadio.FileServer.Catalogue.Presentation.Controllers.V1;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AlbumsController : ControllerBase
{
    private readonly ILogger<AlbumsController> _logger;
    private readonly ISender _sender;
    public AlbumsController(ILogger<AlbumsController> logger, ISender sender)
    {
        _logger = Guard.Against.Null(logger);
        _sender = Guard.Against.Null(sender);
    }

    [HttpGet]
    public async Task<IActionResult> GetAlbumss([FromQuery] AlbumQueryParameters queryParameters)
    {
        _logger.LogInformation("Get Albums method called");
        PageList<AlbumResponseDto> albums;
        var albumName = queryParameters.AlbumName;
        var artistName = queryParameters.ArtistName;
        if (artistName != null && albumName != null)
            albums = await _sender.Send(new GetPaginatedAlbumsByArtistAndAlbumNamesQuery(queryParameters, artistName, albumName));
        else
            albums = await _sender.Send(new GetPaginatedAlbumsQuery(queryParameters));
        if (albums is null)
        {
            return NotFound();
        }
        return Ok(albums);
    }

    [HttpGet("album/{id:length(36)}")]
    public async Task<IActionResult> GetAlbumById(string id)
    {
        _logger.LogInformation("Get Album by Id method called");
        var album = await _sender.Send(new GetAlbumByIdQuery(id));
        if (album is null)
        {
            return NotFound();
        }
        return Ok(album.ToResponseDto());
    }

    [HttpPost("album")]
    public async Task<IActionResult> AddAlbum([FromForm] AlbumInputDto albumInput)
    {
        _logger.LogInformation("Post Album method called");

        var addAlbumCommand = new AddAlbumCommand(albumInput);
        var albumId = await _sender.Send(addAlbumCommand);
        return CreatedAtAction("AddAlbum", new { id = albumId });
    }

    [HttpPut("album/{id:length(36)}")]
    public async Task<IActionResult> UpdateAlbum(string id, AlbumInputDto albumInput)
    {
        _logger.LogInformation("Update Album method called");
        var updateAlbumCommand = new UpdateAlbumCommand
            (
                Guid.Parse(id),
                albumInput
            );
        var result = await _sender.Send(updateAlbumCommand);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("album/{id:length(36)}")]
    public async Task<IActionResult> RemoveAlbum(string id)
    {
        _logger.LogInformation("Remove Album method called");
        var removeAlbumCommand = new RemoveAlbumCommand(Guid.Parse(id));
        var result = await _sender.Send(removeAlbumCommand);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
