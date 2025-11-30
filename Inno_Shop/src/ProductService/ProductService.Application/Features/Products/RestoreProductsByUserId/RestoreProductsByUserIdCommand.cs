using MediatR;

namespace ProductService.Application.Features.Products.RestoreProductsByUserId;

public record RestoreProductsByUserIdCommand(Guid UserId): IRequest;