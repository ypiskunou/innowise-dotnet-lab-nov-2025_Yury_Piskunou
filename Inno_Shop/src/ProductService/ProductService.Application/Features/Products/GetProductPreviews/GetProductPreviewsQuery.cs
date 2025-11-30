using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.GetProductPreviews;

public record GetProductPreviewsQuery() : IRequest<IEnumerable<ProductPreviewDto>>;