
namespace UserService.Shared.DataTransferObjects;

public record TokenDto(
    string AccessToken,
    string RefreshToken
);