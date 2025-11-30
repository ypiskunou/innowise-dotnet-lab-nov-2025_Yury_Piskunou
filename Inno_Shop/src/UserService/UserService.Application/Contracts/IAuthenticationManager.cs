using UserService.Entities.Models;
using UserService.Shared.DataTransferObjects;

namespace UserService.Application.Contracts;

public interface IAuthenticationManager
{
    Task<TokenDto> CreateTokenAsync(User user);
}