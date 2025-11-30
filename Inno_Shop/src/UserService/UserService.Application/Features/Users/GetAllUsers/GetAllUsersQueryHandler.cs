using AutoMapper;
using MediatR;
using UserService.Contracts;
using UserService.Shared.DataTransferObjects;
using UserService.Shared.RequestFeatures;

namespace UserService.Application.Features.Users.GetAllUsers;

public class GetAllUsersQueryHandler 
    : IRequestHandler<GetAllUsersQuery, (IEnumerable<UserDto> users, MetaData metaData)>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<UserDto> users, MetaData metaData)> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var usersWithMetaData = await _repository.User
            .GetAllUsersAsync(request.UserParameters, trackChanges: false);
        
        var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersWithMetaData);

        return (users: usersDto, metaData: usersWithMetaData.MetaData);
    }
}