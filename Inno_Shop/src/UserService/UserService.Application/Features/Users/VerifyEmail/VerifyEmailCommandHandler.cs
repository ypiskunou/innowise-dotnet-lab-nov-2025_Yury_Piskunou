using MediatR;
using UserService.Contracts;
using UserService.Entities.Exceptions;

namespace UserService.Application.Features.Users.VerifyEmail;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, bool>
{
    private readonly IRepositoryManager _repository;

    public VerifyEmailCommandHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.User.GetUserByEmailAsync(request.Email, trackChanges: true);
        
        if (user is null)
        {
            throw new BadRequestException("Invalid Request");
        }
        
        if (user.VerificationToken != request.Token)
        {
            throw new BadRequestException("Invalid Token");
        }
        
        user.VerifiedAt = DateTime.UtcNow;
        user.VerificationToken = null; 
        
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}