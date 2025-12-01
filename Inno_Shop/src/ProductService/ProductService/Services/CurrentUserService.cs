using System.Security.Claims;
using ProductService.Contracts;

namespace ProductService.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (Guid.TryParse(userIdString, out var userId))
            {
                return userId;
            }
            
            return null;
        }
    }
    
    public bool IsActive
    {
        get
        {
            var isActiveString = _httpContextAccessor.HttpContext?.User?.FindFirstValue("IsActive");
            
            return bool.TryParse(isActiveString, out var isActive) && isActive;
        }
    }
}