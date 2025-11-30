namespace UserService.Shared.DataTransferObjects;

public record UserForRegistrationDto(
    string Name,
    string Email,
    string Password,
    string ConfirmPassword
);