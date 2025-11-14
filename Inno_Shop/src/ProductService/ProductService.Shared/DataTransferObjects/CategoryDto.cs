namespace ProductService.Shared.DataTransferObjects;

public record CategoryDto(
    Guid Id,
    string Name,
    string? Description
);