using AutoMapper;
using MediatR;
using Shared.DataTransferObjects;
using UserService.Contracts;

namespace UserService.Application.Features.Users.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.User.GetAllUsersAsync(trackChanges: false);
        
        var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

        return usersDto;
    }
}