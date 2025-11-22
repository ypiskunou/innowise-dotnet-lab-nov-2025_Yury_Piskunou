using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using UserService.Application.Features.Authentication.AuthenticateUser;

namespace UserService.Presentation.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;
    public AuthenticationController(ISender sender) => _sender = sender;

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuth)
    {
        var tokenDto = await _sender.Send(new AuthenticateUserQuery(userForAuth));
        return Ok(tokenDto);
    }
}