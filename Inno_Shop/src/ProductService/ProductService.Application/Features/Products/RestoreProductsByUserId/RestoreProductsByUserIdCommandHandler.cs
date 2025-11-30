using MediatR;
using ProductService.Contracts;

namespace ProductService.Application.Features.Products.RestoreProductsByUserId;

public class RestoreProductsByUserIdCommandHandler: IRequestHandler<RestoreProductsByUserIdCommand>
{
    private readonly IRepositoryManager _repository;

    public RestoreProductsByUserIdCommandHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(RestoreProductsByUserIdCommand request, CancellationToken cancellationToken)
    {
        var products = await _repository.Product
            .GetProductsByUserIdAsync(request.UserId, trackChanges: true, true);

        foreach (var product in products)
        {
            product.IsDeleted = false; 
        }

        await _repository.SaveChangesAsync(cancellationToken);
    }
}