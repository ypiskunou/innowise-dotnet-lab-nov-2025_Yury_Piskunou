
namespace ProductService.Shared.DataTransferObjects;

public record ProductForCreationDto(
    string Name, 
    decimal Price, 
    string Description, 
    Guid CategoryId 
);