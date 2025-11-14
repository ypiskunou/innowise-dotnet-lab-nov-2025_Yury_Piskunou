using AutoMapper;
using MediatR;
using Shared.DataTransferObjects;
using UserService.Contracts;
using UserService.Entities.Exceptions;

namespace UserService.Application.Features.Users.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.User.GetUserByIdAsync(request.Id, trackChanges: false);
        
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        
        var userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }
}