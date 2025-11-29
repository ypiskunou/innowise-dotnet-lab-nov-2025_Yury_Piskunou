using MediatR;
using ProductService.Shared.DataTransferObjects;
using ProductService.Shared.RequestFeatures;

namespace ProductService.Application.Features.Products.GetAllProducts;

public record GetAllProductsQuery(ProductParameters ProductParameters) 
    : IRequest<(IEnumerable<ProductDto> products, MetaData metaData)>;