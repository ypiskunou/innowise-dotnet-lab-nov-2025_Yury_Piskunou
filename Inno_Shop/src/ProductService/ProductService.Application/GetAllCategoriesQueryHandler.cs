using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application;

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
        var categories = await _repository.Category.GetAllCategoriesAsync(trackChanges: false);
        
        var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
        
        return categoriesDto;
    }
}