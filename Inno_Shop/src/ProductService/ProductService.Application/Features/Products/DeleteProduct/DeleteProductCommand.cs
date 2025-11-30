using MediatR;

namespace ProductService.Application.Features.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;