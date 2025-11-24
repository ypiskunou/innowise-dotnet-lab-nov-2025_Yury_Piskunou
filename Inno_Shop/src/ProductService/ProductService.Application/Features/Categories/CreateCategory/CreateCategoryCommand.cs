using MediatR;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Application.Features.Categories.CreateCategory;

public record CreateCategoryCommand(CategoryForCreationDto CategoryDto) : IRequest<CategoryDto>;