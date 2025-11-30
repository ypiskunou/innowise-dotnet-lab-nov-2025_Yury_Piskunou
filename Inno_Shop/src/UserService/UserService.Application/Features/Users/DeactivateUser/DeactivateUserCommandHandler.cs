using MediatR;
using Microsoft.Extensions.Configuration;
using UserService.Contracts;
using UserService.Entities.Exceptions;

namespace UserService.Application.Features.Users.DeactivateUser;

public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand>
{
    private readonly IRepositoryManager _repository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public DeactivateUserCommandHandler(IRepositoryManager repository, IHttpClientFactory httpClientFactory, 
        IConfiguration configuration)
    {
        _repository = repository;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await _repository.User.GetUserByIdAsync(request.Id, trackChanges: true);
        if (userEntity is null) throw new UserNotFoundException(request.Id);
        
        userEntity.IsActive = false; 
        await _repository.SaveChangesAsync(cancellationToken);
        
        var client = _httpClientFactory.CreateClient();
        var baseUrl = _configuration["ApiUrls:ProductService"];
        var url = $"{baseUrl}/api/products/hide-by-user/{request.Id}";

        try 
        {
            await client.PutAsync(url, null, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calling ProductService: {ex.Message}");
        }
    }
}
