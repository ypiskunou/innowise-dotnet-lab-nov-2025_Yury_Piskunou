using Entities;
using Microsoft.EntityFrameworkCore;
using UserService.Contracts;
using UserService.Entities.Models;

namespace UserService.Repository;

public class RoleRepository: RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Role?>> GetAllRolesAsync(bool trackChanges) => await FindAll(trackChanges)
        .OrderBy(r => r.Name)
        .ToListAsync();

    public async Task<Role?> GetRoleByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(b => b.Id == id, trackChanges).FirstOrDefaultAsync();
    
    public async Task<Role?> GetRoleByNameAsync(string name, bool trackChanges) =>
        await FindByCondition(r => r.Name.Equals(name), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateRole(Role role) => Create(role);

    public void DeleteRole(Role role) => Delete(role);
}