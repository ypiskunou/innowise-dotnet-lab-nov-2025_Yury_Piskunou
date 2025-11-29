using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Shared.DataTransferObjects;
using ProductService.Shared.RequestFeatures;

namespace ProductService.Application.Features.Products.GetAllProducts;

public class GetAllProductsQueryHandler 
    : IRequestHandler<GetAllProductsQuery, (IEnumerable<ProductDto> products, MetaData metaData)>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<(IEnumerable<ProductDto> products, MetaData metaData)> Handle(GetAllProductsQuery request, 
        CancellationToken cancellationToken)
    {
        var productsWithMetaData = await _repository.Product
            .GetAllProductsAsync(request.ProductParameters, trackChanges: false);

        var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsWithMetaData);
        
        return (products: productsDto, metaData: productsWithMetaData.MetaData);
    }
}