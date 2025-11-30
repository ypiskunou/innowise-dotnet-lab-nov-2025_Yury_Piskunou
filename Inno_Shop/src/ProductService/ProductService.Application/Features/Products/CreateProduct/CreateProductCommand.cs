using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.CreateProduct;

public record CreateProductCommand(ProductForCreationDto Product) : IRequest<ProductDto>;