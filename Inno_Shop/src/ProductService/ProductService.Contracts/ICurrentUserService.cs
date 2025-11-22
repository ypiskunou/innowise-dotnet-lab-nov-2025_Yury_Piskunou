namespace ProductService.Contracts;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}