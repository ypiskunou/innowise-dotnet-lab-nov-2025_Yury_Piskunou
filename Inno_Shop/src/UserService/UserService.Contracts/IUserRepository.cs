using System.Linq.Expressions;
using Entities;
using UserService.Entities.Models;
using UserService.Shared.RequestFeatures;

namespace UserService.Contracts;

public interface IUserRepository
{
    Task<PagedList<User?>> GetAllUsersAsync(UserParameters userParameters, bool trackChanges);
    Task<User?> GetUserByIdAsync(Guid id, bool trackChanges);
    Task<User?> GetUserByEmailAsync(string email, bool trackChanges);
    void CreateUser(User user);
    void DeleteUser(User user);
}