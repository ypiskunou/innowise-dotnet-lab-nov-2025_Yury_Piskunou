using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Application;
using UserService.Application.Contracts;
using UserService.Application.Features.Authentication;
using UserService.Application.Services;
using UserService.Contracts;
using UserService.Entities.Models;
using UserService.Repository;

namespace UserService.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
    
    public static void ConfigureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>(); 
    }
    
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MappingProfile).Assembly));
        
        services.AddValidatorsFromAssembly(typeof(MappingProfile).Assembly);

        services.AddScoped<IAuthenticationManager, AuthenticationManager>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        
        services.AddScoped<IEmailSender, EmailSender>();

        // Здесь же можно будет добавить Pipeline Behaviors
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
    
    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Key"];

        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
    }
}