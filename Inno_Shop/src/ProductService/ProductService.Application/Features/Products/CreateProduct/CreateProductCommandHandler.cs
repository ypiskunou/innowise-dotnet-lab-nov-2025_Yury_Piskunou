using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Entities.Exceptions;
using ProductService.Entities.Models;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Products.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateProductCommandHandler(IRepositoryManager repository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        if (currentUserId is null)
        {
            throw new UnauthorizedAccessException();
        }
        
        if (!_currentUserService.IsActive)
        {
            throw new ForbiddenException("Ваш аккаунт деактивирован. Вы не можете создавать новые товары.");
        }
        
        var category = await _repository.Category
            .GetCategoryByIdAsync(request.Product.CategoryId, trackChanges: false, cancellationToken);
        if (category is null)
        {
            throw new CategoryNotFoundException(request.Product.CategoryId);
        }
        
        var productEntity = _mapper.Map<Product>(request.Product);
        
        productEntity.UserId = currentUserId.Value;
        productEntity.IsActive = true;
        productEntity.IsDeleted = false;
        productEntity.DateOfCreation = DateTime.UtcNow;
        
        _repository.Product.CreateProduct(productEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        
        productEntity.Category = category;
        var productToReturn = _mapper.Map<ProductDto>(productEntity);

        return productToReturn;
    }
}