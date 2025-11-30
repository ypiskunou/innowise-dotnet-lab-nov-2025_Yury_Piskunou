namespace UserService.Shared.DataTransferObjects;

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    IEnumerable<string> Roles 
);