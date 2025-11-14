namespace ProductService.Shared.DataTransferObjects;

public record ProductForUpdateDto(
    string Name, 
    decimal Price, 
    string Description, 
    Guid CategoryId
);