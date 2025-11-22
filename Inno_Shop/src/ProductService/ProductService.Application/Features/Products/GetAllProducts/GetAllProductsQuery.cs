using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.GetAllProducts;

public record GetAllProductsQuery(): IRequest<IEnumerable<ProductDto>>;