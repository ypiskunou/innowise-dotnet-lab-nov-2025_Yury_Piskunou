using Shared.DataTransferObjects;
using UserService.Entities.Models;

namespace UserService.Service.Contracts;

public interface IAuthenticationManager
{
    Task<TokenDto> CreateTokenAsync(User user);
}