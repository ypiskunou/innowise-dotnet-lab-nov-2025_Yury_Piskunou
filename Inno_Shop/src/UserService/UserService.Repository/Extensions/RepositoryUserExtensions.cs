using UserService.Entities.Models;

namespace UserService.Repository.Extensions;

public static class RepositoryUserExtensions
{
    public static IQueryable<User> FilterUsers(this IQueryable<User> users, bool? isActive, Guid? roleId)
    {
        if (isActive.HasValue)
        {
            users = users.Where(u => u.IsActive == isActive.Value);
        }
        
        if (roleId.HasValue)
        {
            users = users.Where(u => u.Roles.Any(r => r.Id == roleId.Value));
        }

        return users;
    }
    
    public static IQueryable<User> Search(this IQueryable<User> users, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return users;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        
        return users.Where(u => 
            u.Name.ToLower().Contains(lowerCaseTerm) || 
            u.Email.ToLower().Contains(lowerCaseTerm));
    }
    
    public static IQueryable<User> Sort(this IQueryable<User> users, string? orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return users.OrderBy(u => u.Name);

        var orderQuery = orderByQueryString.Trim().ToLower();

        return orderQuery switch
        {
            "name desc" => users.OrderByDescending(u => u.Name),
            "email" => users.OrderBy(u => u.Email),
            "email desc" => users.OrderByDescending(u => u.Email),
            _ => users.OrderBy(u => u.Name)
        };
    }
}