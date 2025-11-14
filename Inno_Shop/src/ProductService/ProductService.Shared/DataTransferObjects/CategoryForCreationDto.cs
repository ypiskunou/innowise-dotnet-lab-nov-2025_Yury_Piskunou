
namespace ProductService.Shared.DataTransferObjects;

public record CategoryForCreationDto(
    string Name,
    string? Description
);