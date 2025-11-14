namespace Shared.DataTransferObjects;

public record UserForAuthenticationDto(
    string Email,
    string Password
);