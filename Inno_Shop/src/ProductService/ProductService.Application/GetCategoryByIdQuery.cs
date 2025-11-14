using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;