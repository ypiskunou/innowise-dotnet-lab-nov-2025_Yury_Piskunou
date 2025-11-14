namespace ProductService.Shared.DataTransferObjects;

public record CategoryForUpdateDto(
    string Name,
    string? Description
);