using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Contracts;
using UserService.Entities.Exceptions;
using UserService.Entities.Models;
using UserService.Service.Features.Users.RegisterUser;

namespace UserService.Application.Features.Users.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public RegisterUserCommandHandler(IRepositoryManager repository, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userWithSameEmail = await _repository.User.GetUserByEmailAsync(request.UserForRegistration.Email, false);
        if (userWithSameEmail != null)
            throw new EmailAlreadyExistsException(request.UserForRegistration.Email);
        
        var userEntity = _mapper.Map<User>(request.UserForRegistration);
        
        userEntity.PasswordHash = _passwordHasher.HashPassword(userEntity, request.UserForRegistration.Password);
        
        var defaultRole = await _repository.Role.GetRoleByNameAsync("User", true);
        if(defaultRole is null) throw new RoleNotFoundException("User"); 
        userEntity.Roles.Add(defaultRole);
        
        _repository.User.CreateUser(userEntity);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}