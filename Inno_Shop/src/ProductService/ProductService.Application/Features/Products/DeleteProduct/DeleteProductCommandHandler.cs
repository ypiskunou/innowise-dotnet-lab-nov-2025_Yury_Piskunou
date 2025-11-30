using MediatR;
using ProductService.Contracts;
using ProductService.Entities.Exceptions;

namespace ProductService.Application.Features.Products.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IRepositoryManager _repository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteProductCommandHandler(IRepositoryManager repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = await _repository.Product
            .GetProductByIdAsync(request.Id, trackChanges: false, cancellationToken);
        if (productEntity is null)
        {
            return;
        }
        
        var currentUserId = _currentUserService.UserId;
        if (currentUserId is null)
        {
            throw new UnauthorizedAccessException();
        }
        
        if (productEntity.UserId != currentUserId.Value)
        {
            throw new ForbiddenException("You can only delete your own products.");
        }
        
        _repository.Product.DeleteProduct(productEntity);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}