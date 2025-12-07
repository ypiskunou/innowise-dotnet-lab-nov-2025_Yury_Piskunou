using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Entities.Exceptions;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public GetProductByIdQueryHandler(IRepositoryManager repository, IMapper mapper, 
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        // 1. Достаем "сырой" товар (Repository должен быть с IgnoreQueryFilters!)
        var product = await _repository.Product.GetProductByIdAsync(request.Id, trackChanges: false, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(request.Id);
        }

        // 2. Проверяем статус товара
        // Товар считается "скрытым", если он удален ИЛИ не активен
        bool isHidden = product.IsDeleted || !product.IsActive;

        // 3. Если товар скрыт, начинаем проверку прав
        if (isHidden)
        {
            var currentUserId = _currentUser.UserId;
            
            // Если юзер не залогинен - сразу нет
            if (currentUserId is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            // Проверяем: это Владелец?
            bool isOwner = product.UserId == currentUserId.Value;
            
            // Проверяем: это Админ? (нужно будет добавить метод IsInRole в CurrentUser, или пока упростить)
            // Допустим, пока проверяем только владельца.
            // Если требуется проверка админа, нужно добавить Claim "role" в CurrentUserService.
            
            if (!isOwner) 
            {
                // Если это чужой скрытый товар — кидаем 404 (NotFound), 
                // чтобы никто даже не знал, что такой ID существует.
                throw new ProductNotFoundException(request.Id);
            }
        }

        // 4. Если мы здесь — значит либо товар публичный, либо это владелец
        return _mapper.Map<ProductDto>(product);
    }
}