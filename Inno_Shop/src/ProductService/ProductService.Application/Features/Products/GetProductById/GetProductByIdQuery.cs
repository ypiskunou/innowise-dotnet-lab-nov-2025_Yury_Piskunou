using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;