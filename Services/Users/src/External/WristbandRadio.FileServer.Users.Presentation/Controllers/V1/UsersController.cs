namespace WristbandRadio.FileServer.Users.Presentation.Controllers.V1;
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly ISender _sender;
    public UsersController(ILogger<UsersController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UsersQueryParameters queryParameters)
    {
        _logger.LogInformation("Get Users method called");
        var users = await _sender.Send(new GetPaginatedUsersQuery(queryParameters));
        if (users is null)
        {
            return NotFound();
        }
        return Ok(users);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserByEmail([FromQuery] UsersQueryParameters queryParameters)
    {
        _logger.LogInformation("Get Users by Email method called");
        var email = queryParameters.Email;
        var user = email != null
            ? await _sender.Send(new GetUserByEmailQuery(queryParameters, email))
            : null;
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user.ToResponseDto());
    }

    [HttpGet("user/{id:length(36)}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        _logger.LogInformation("Get user by Id method called");
        var feedbackSubmission = await _sender.Send(new GetUserByIdQuery(id));
        if (feedbackSubmission is null)
        {
            return NotFound();
        }
        return Ok(feedbackSubmission.ToResponseDto());
    }

    [HttpPost("user")]
    public async Task<IActionResult> AddUser([FromBody] UserInputDto userInput)
    {
        _logger.LogInformation("Post user method called");

        var createUserCommand = new CreateUserCommand(userInput);
        var newUser = await _sender.Send(createUserCommand);
        return CreatedAtAction("AddUser", newUser.ToResponseDto());
    }

    [HttpPut("user/{id:length(36)}")]
    public async Task<IActionResult> UpdateUserName(string id, [FromBody] UpdateUserNameDto updateUserNameDto)
    {
        _logger.LogInformation("Update user name method called");
        var updateUserNameCommand = new UpdateUserNameCommand
            (
                id,
                updateUserNameDto.FirstName,
                updateUserNameDto.LastName
            );
        var result = await _sender.Send(updateUserNameCommand);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
