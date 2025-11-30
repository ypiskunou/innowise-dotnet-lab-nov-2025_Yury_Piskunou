using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Entities.Exceptions;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Categories.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.Category.GetCategoryByIdAsync(request.Id, trackChanges: false);
        
        if (category is null)
        {
            throw new CategoryNotFoundException(request.Id);
        }
        
        var categoryDto = _mapper.Map<CategoryDto>(category);
        return categoryDto;
    }
}