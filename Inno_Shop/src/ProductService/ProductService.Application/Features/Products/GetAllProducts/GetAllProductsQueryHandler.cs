using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.Product.GetAllProductsAsync(false);
        var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
        return productsDto;
    }
}