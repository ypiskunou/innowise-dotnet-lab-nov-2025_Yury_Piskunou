using MediatR;

namespace ProductService.Application.Features.Categories.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest;