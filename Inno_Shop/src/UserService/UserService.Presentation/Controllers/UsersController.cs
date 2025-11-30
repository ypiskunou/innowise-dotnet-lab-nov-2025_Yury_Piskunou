using System.Security.Claims;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Features.Users.ActivateUser;
using UserService.Application.Features.Users.DeactivateUser;
using UserService.Application.Features.Users.GetAllUsers;
using UserService.Application.Features.Users.GetUserById;
using UserService.Application.Features.Users.RegisterUser;
using UserService.Application.Features.Users.UpdateUserProfile;
using UserService.Application.Features.Users.VerifyEmail;
using UserService.Shared.DataTransferObjects;
using UserService.Shared.RequestFeatures;

namespace UserService.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController: ControllerBase
{
    private readonly ISender _sender;
    public UsersController(ISender sender) => _sender = sender;

    [ProducesResponseType(StatusCodes.Status204NoContent)] 
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        await _sender.Send(new RegisterUserCommand(userForRegistration));
        return NoContent(); 
    }
    
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        
        var userDto = await _sender.Send(new GetUserByIdQuery(Guid.Parse(userId)));

        return Ok(userDto);
    }
    
    [HttpPut("me")]
    [Authorize]
    public async Task<IActionResult> UpdateMyProfile([FromBody] UserForUpdateDto userForUpdate)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized();
        }
        
        var command = new UpdateUserProfileCommand(userId, userForUpdate);
        await _sender.Send(command);

        return NoContent();
    }
       
    
    // --- ADMIN-ONLY ENDPOINTS ---
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers([FromQuery] UserParameters userParameters)
    {
        var pagedResult = await _sender.Send(new GetAllUsersQuery(userParameters));
    
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.users);
    }
    
    [HttpGet("{id:guid}", Name = "GetUserById")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var userDto = await _sender.Send(new GetUserByIdQuery(id));
        return Ok(userDto);
    }
    
    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string token)
    {
        var command = new VerifyEmailCommand(email, token);
        await _sender.Send(command);
        
        return Ok("Email confirmed successfully! Now you can login.");
    }
    
    [HttpPut("{id:guid}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeactivateUser(Guid id)
    {
        await _sender.Send(new DeactivateUserCommand(id));
        return NoContent();
    }
    
    [HttpPut("{id:guid}/activate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ActivateUser(Guid id)
    {
        await _sender.Send(new ActivateUserCommand(id));
        return NoContent();
    }
}