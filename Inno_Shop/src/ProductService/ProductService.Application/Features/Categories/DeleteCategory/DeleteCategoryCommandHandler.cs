using MediatR;
using ProductService.Contracts;
using ProductService.Entities.Exceptions;

namespace ProductService.Application.Features.Categories.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IRepositoryManager _repository;

    public DeleteCategoryCommandHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.Category
            .GetCategoryByIdAsync(request.Id, trackChanges: false, cancellationToken);
        if (category is null) throw new CategoryNotFoundException(request.Id);
        
        var hasProducts = await _repository.Product.HasProductsInCategoryAsync(request.Id, cancellationToken);

        if (hasProducts)
        {
            throw new BadRequestException($"Cannot delete category with id {request.Id} because it contains products.");
        }
        
        _repository.Category.DeleteCategory(category);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}