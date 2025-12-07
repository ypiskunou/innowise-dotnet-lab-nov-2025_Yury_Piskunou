using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Categories.GetAllCategories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _repository.Category
            .GetAllCategoriesWithCountsAsync(trackChanges: false);
        
        var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
        
        return categoriesDto;
    }
}