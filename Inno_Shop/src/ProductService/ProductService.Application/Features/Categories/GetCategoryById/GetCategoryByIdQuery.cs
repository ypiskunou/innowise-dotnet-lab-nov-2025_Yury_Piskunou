using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Categories.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;