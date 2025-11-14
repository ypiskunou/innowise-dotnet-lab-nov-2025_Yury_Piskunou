using System.Linq.Expressions;
using Entities;
using UserService.Entities.Models;

namespace UserService.Contracts;

public interface IUserRepository
{
    Task<IEnumerable<User?>> GetAllUsersAsync(bool trackChanges);
    Task<User?> GetUserByIdAsync(Guid id, bool trackChanges);
    Task<User?> GetUserByEmailAsync(string email, bool trackChanges);
    void CreateUser(User user);
    void DeleteUser(User user);
    
    Task<IEnumerable<User?>> SearchUsersByNameAsync(string name, bool trackChanges);
    
    IQueryable<User?> GetUsersWithRoles(bool trackChanges);
    Task<IEnumerable<T>> GetUsersAsAsync<T>(IQueryable<User?> users, Expression<Func<User, T>> selector);
    
}