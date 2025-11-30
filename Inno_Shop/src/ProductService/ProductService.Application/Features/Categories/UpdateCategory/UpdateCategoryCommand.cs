using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Categories.UpdateCategory;

public record UpdateCategoryCommand(Guid Id, CategoryForUpdateDto CategoryDto) : IRequest;