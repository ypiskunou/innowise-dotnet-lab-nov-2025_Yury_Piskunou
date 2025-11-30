using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Entities.Models;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Categories.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryEntity = _mapper.Map<Category>(request.CategoryDto);
        
        _repository.Category.CreateCategory(categoryEntity);
        await _repository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CategoryDto>(categoryEntity);
    }
}