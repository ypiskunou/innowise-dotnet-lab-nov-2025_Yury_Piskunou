using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Categories.GetAllCategories;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;