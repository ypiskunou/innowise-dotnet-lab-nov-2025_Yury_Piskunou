using AutoMapper;
using MediatR;
using UserService.Contracts;
using UserService.Entities.Exceptions;

namespace UserService.Application.Features.Users.UpdateUserProfile;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public UpdateUserProfileCommandHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await _repository.User.GetUserByIdAsync(request.UserId, trackChanges: true);

        if (userEntity is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        
        _mapper.Map(request.Dto, userEntity);
        
        await _repository.SaveChangesAsync(cancellationToken);
    }
}