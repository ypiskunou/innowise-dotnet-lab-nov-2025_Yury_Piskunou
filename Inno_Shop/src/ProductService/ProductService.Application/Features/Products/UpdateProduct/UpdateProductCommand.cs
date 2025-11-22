using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, ProductForUpdateDto Product) : IRequest;