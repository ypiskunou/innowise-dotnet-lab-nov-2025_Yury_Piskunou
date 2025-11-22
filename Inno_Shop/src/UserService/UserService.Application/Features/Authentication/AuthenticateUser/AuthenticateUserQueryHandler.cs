using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;
using UserService.Application.Contracts;
using UserService.Contracts;
using UserService.Entities.Exceptions;
using UserService.Entities.Models;

namespace UserService.Application.Features.Authentication.AuthenticateUser;

public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, TokenDto>
{
    private readonly IRepositoryManager _repository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IAuthenticationManager _authManager;

    public AuthenticateUserQueryHandler(
        IRepositoryManager repository, 
        IPasswordHasher<User> passwordHasher, 
        IAuthenticationManager authManager)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _authManager = authManager;
    }

    public async Task<TokenDto> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.User.GetUserByEmailAsync(request.UserForAuth.Email, trackChanges: false);

        if (user is null)
            throw new InvalidPasswordException();
        
        var passwordVerificationResult = 
            _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.UserForAuth.Password);
        
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            throw new InvalidPasswordException();
        
        var tokenDto = await _authManager.CreateTokenAsync(user);

        return tokenDto;
    }
}