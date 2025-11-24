using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Entities.Exceptions;

namespace ProductService.Application.Features.Categories.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryEntity = await _repository.Category
            .GetCategoryByIdAsync(request.Id, trackChanges: true, cancellationToken);

        if (categoryEntity is null)
        {
            throw new CategoryNotFoundException(request.Id);
        }
        
        _mapper.Map(request.CategoryDto, categoryEntity);
        
        await _repository.SaveChangesAsync(cancellationToken);
    }
}