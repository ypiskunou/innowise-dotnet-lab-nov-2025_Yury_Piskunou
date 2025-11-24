using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Entities.Exceptions;

namespace ProductService.Application.Features.Products.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateProductCommandHandler(IRepositoryManager repository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = await _repository.Product.GetProductByIdAsync(request.Id, trackChanges: true);
        if (productEntity is null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        var currentUserId = _currentUserService.UserId;
        
        if (currentUserId is null)
        {
            throw new UnauthorizedAccessException();
        }
        
        if (productEntity.UserId != currentUserId.Value)
        {
            throw new ForbiddenException("You can only update your own products.");
        }

        _mapper.Map(request.Product, productEntity);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}