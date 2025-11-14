using System.Linq.Expressions;
using Entities;
using Microsoft.EntityFrameworkCore;
using UserService.Contracts;
using UserService.Entities.Models;
using UserService.Repository;

namespace Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<User?>> GetAllUsersAsync(bool trackChanges) => await FindAll(trackChanges)
        .OrderBy(u => u.Name)
        .ToListAsync();

    public async Task<IEnumerable<User?>> SearchUsersByNameAsync(string name, bool trackChanges) =>
        await FindByCondition(u => u.Name.ToLower().Contains(name.ToLower().Trim()), trackChanges)
            .OrderBy(a => a.Name)
            .ToListAsync();

    public IQueryable<User?> GetUsersWithRoles(bool trackChanges) => 
        FindAll(trackChanges)
            .OrderBy(u => u.Name)
            .Include(u => u.Roles);

    public async Task<IEnumerable<T>> GetUsersAsAsync<T>(IQueryable<User?> users,
        Expression<Func<User, T>> selector)
    {
        return await users.Select(selector).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(a => a.Id == id, trackChanges)
            .FirstOrDefaultAsync();
    
    public async Task<User?> GetUserByEmailAsync(string email, bool trackChanges) =>
        await FindByCondition(u => u.Email.Equals(email), trackChanges)
            .Include(u => u.Roles) 
            .SingleOrDefaultAsync();

    public void CreateUser(User user) => Create(user);

    public void DeleteUser(User user) => Delete(user);
}