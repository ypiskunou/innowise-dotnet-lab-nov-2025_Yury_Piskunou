
using UserService.Entities.Models;

namespace UserService.Contracts;

public interface IRoleRepository
{
    Task<IEnumerable<Role?>> GetAllRolesAsync(bool trackChanges);
    Task<Role?> GetRoleByIdAsync(Guid id, bool trackChanges);
    Task<Role?> GetRoleByNameAsync(string name, bool trackChanges);
    void CreateRole(Role role);
    void DeleteRole(Role role);
}