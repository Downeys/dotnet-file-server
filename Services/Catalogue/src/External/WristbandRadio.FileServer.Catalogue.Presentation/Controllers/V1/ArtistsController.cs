namespace WristbandRadio.FileServer.Catalogue.Presentation.Controllers.V1;
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ArtistsController : ControllerBase
{
    private readonly ILogger<ArtistsController> _logger;
    private readonly ISender _sender;
    public ArtistsController(ILogger<ArtistsController> logger, ISender sender)
    {
        _logger = Guard.Against.Null(logger);
        _sender = Guard.Against.Null(sender);
    }

    [HttpGet]
    public async Task<IActionResult> GetArtists([FromQuery] ArtistQueryParameters queryParameters)
    {
        _logger.LogInformation("Get Artists method called");
        PageList<ArtistResponseDto> artists;
        var artistName = queryParameters.ArtistName;
        var hometownZipCode = queryParameters.HometownZipCode;
        if (artistName != null && hometownZipCode != null)
            artists = await _sender.Send(new GetPaginatedArtistsByNameAndHometownZipCodeQuery(queryParameters, artistName, hometownZipCode));
        else
            artists = await _sender.Send(new GetPaginatedArtistsQuery(queryParameters));
        if (artists is null)
        {
            return NotFound();
        }
        return Ok(artists);
    }

    [HttpGet("artist/{id:length(36)}")]
    public async Task<IActionResult> GetArtistById(string id)
    {
        _logger.LogInformation("Get Artist by Id method called");  
        var artist = await _sender.Send(new GetArtistByIdQuery(id));
        if (artist is null)
        {
            return NotFound();
        }
        return Ok(artist.ToResponseDto());
    }

    [HttpPost("artist")]
    public async Task<IActionResult> AddArtist([FromForm] ArtistInputDto artistInput)
    {
        _logger.LogInformation("Post Artist method called");

        var addArtistCommand = new AddArtistCommand(artistInput.ArtistName, artistInput.HometownZipCode, artistInput.CurrentZipCode);
        var artistId = await _sender.Send(addArtistCommand);
        return CreatedAtAction("AddArtist", new { id = artistId });
    }

    [HttpPut("artist/{id:length(36)}")]
    public async Task<IActionResult> UpdateArtist(string id, ArtistInputDto artistInput)
    {
        _logger.LogInformation("Update Artist method called");
        var updateArtistCommand = new UpdateArtistCommand
            (
                Guid.Parse(id),
                artistInput
            );
        var result = await _sender.Send(updateArtistCommand);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("artist/{id:length(36)}")]
    public async Task<IActionResult> RemoveArtist(string id)
    {
        _logger.LogInformation("Remove Artist method called");
        var removeArtistCommand = new RemoveArtistCommand(Guid.Parse(id));
        var result = await _sender.Send(removeArtistCommand);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
