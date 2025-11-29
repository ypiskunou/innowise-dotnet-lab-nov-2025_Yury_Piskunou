using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.GetMyProducts;

public record GetMyProductsQuery() : IRequest<IEnumerable<ProductDto>>;