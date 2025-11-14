using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application;

public record GetAllProductsQuery(): IRequest<IEnumerable<ProductDto>>;