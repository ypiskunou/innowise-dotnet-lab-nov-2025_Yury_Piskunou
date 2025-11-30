using MediatR;

namespace ProductService.Application.Features.Products.HideProductsByUserId;

public record HideProductsByUserIdCommand(Guid UserId) : IRequest;