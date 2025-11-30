namespace UserService.Shared.RequestFeatures;

public class UserParameters : RequestParameters
{
    public UserParameters()
    {
        OrderBy = "name"; 
    }
    
    public bool? IsActive { get; set; }
    public Guid? RoleId { get; set; }
    
    public string? SearchTerm { get; set; }
}