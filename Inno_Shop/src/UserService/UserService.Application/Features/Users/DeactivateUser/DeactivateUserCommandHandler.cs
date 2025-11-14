using MediatR;
using UserService.Contracts;
using UserService.Entities.Exceptions;

namespace UserService.Application.Features.Users.DeactivateUser;

public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand>
{
    private readonly IRepositoryManager _repository;

    public DeactivateUserCommandHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await _repository.User.GetUserByIdAsync(request.Id, trackChanges: true);

        if (userEntity is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        
        userEntity.IsActive = false; 
        
        await _repository.SaveChangesAsync(cancellationToken);
    }
}