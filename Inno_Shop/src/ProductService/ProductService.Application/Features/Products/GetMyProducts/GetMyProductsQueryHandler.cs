using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.GetMyProducts;

public class GetMyProductsQueryHandler : IRequestHandler<GetMyProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser; // Наш сервис

    public GetMyProductsQueryHandler(IRepositoryManager repository, IMapper mapper, ICurrentUserService currentUser)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetMyProductsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        if (userId is null) throw new UnauthorizedAccessException();
        
        var products = await _repository.Product
            .GetProductsByUserIdAsync(userId.Value, trackChanges: false, true, cancellationToken);
        
        var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

        return productsDto;
    }
}