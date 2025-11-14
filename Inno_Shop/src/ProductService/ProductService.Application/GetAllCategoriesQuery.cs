using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;