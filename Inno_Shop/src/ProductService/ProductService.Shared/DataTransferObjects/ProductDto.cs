namespace ProductService.Shared.DataTransferObjects;

public record ProductDto(
    Guid Id, 
    string Name, 
    decimal Price, 
    string Description, 
    string CategoryName,
    Guid CategoryId, 
    bool IsActive
);