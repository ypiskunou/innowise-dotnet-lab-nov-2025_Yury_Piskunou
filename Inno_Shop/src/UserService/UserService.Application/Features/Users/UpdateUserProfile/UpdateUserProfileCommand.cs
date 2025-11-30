using MediatR;
using UserService.Shared.DataTransferObjects;

namespace UserService.Application.Features.Users.UpdateUserProfile;

public record UpdateUserProfileCommand(Guid UserId, UserForUpdateDto Dto) : IRequest;