using System.Linq.Expressions;
using MediatR;
using ProductService.Contracts;
using ProductService.Entities.Models;
using ProductService.Repository.Extensions;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.GetProductPreviews;

public class GetProductPreviewsQueryHandler : IRequestHandler<GetProductPreviewsQuery, IEnumerable<ProductPreviewDto>>
{
    private readonly IRepositoryManager _repository;

    public GetProductPreviewsQueryHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductPreviewDto>> Handle(GetProductPreviewsQuery request, 
        CancellationToken cancellationToken)
    {
        var query = _repository.Product.GetProductsQuery(trackChanges: false);
        
        query = query.Search(request.SearchTerm);
        
        query = query.Take(10);
        
        Expression<Func<Product, ProductPreviewDto>> selector = p => new ProductPreviewDto(
            p.Id,
            p.Name,
            p.Price,
            p.Category.Name 
        );
        
        return await _repository.Product.GetProductsAsAsync(query, selector);
    }
}