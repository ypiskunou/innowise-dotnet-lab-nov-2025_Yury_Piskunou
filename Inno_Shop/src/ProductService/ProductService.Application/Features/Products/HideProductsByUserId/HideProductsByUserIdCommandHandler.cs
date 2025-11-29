using MediatR;
using ProductService.Contracts;

namespace ProductService.Application.Features.Products.HideProductsByUserId;

public class HideProductsByUserIdCommandHandler : IRequestHandler<HideProductsByUserIdCommand>
{
    private readonly IRepositoryManager _repository;

    public HideProductsByUserIdCommandHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task Handle(HideProductsByUserIdCommand request, CancellationToken cancellationToken)
    {
        var products = await _repository.Product
            .GetProductsByUserIdAsync(request.UserId, trackChanges: true, true);

        foreach (var product in products)
        {
            product.IsDeleted = true; 
        }

        await _repository.SaveChangesAsync(cancellationToken);
    }
}