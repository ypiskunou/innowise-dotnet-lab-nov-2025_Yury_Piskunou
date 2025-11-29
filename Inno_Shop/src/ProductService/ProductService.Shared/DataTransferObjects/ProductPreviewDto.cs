namespace ProductService.Shared.DataTransferObjects;

public record ProductPreviewDto(
    Guid Id,
    string Name,
    decimal Price,
    string CategoryName
);