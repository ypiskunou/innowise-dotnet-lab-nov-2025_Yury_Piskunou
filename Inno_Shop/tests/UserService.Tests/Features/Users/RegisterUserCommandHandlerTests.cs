using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using UserService.Application.Contracts;
using UserService.Application.Features.Users.RegisterUser;
using UserService.Contracts;
using UserService.Entities.Exceptions;
using UserService.Entities.Models;
using UserService.Shared.DataTransferObjects;

namespace UserService.Tests.Features.Users;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IPasswordHasher<User>> _mockHasher;
    private readonly Mock<IEmailSender> _mockEmail;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _mockRepo = new Mock<IRepositoryManager>();
        _mockMapper = new Mock<IMapper>();
        _mockHasher = new Mock<IPasswordHasher<User>>();
        _mockEmail = new Mock<IEmailSender>();

        _handler = new RegisterUserCommandHandler(
            _mockRepo.Object, 
            _mockMapper.Object, 
            _mockHasher.Object, 
            _mockEmail.Object
        );
    }

    [Fact]
    public async Task Handle_Should_CreateUserAndSendEmail_When_EmailIsUnique()
    {
        var command = new RegisterUserCommand(new UserForRegistrationDto("Test", "new@mail.com", "Pa$$w0rd", "Pa$$w0rd"));
        var userEntity = new User { Id = Guid.NewGuid(), Email = "new@mail.com", Name = "Test", PasswordHash = "" };
        var role = new Role { Id = Guid.NewGuid(), Name = "User" };
        
        _mockRepo.Setup(r => r.User.GetUserByEmailAsync(command.UserForRegistration.Email, false))
            .ReturnsAsync((User?)null);
        
        _mockMapper.Setup(m => m.Map<User>(command.UserForRegistration))
            .Returns(userEntity);
        
        _mockHasher.Setup(h => h.HashPassword(userEntity, command.UserForRegistration.Password))
            .Returns("hashed_password");
        
        _mockRepo.Setup(r => r.Role.GetRoleByNameAsync("User", true))
            .ReturnsAsync(role);
        
        await _handler.Handle(command, CancellationToken.None);
        
        _mockRepo.Verify(r => r.User.CreateUser(userEntity), Times.Once);
        
        _mockRepo.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        _mockEmail.Verify(e => e.SendEmailAsync(userEntity.Email, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        
        userEntity.VerificationToken.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task Handle_Should_ThrowException_When_EmailAlreadyExists()
    {
        var command = new RegisterUserCommand(new UserForRegistrationDto("Test", "exist@mail.com", "123", "123"));
        var existingUser = new User { Id = Guid.NewGuid(), Name = "Old", Email = "exist@mail.com", PasswordHash = "" };
        
        _mockRepo.Setup(r => r.User.GetUserByEmailAsync(command.UserForRegistration.Email, false))
            .ReturnsAsync(existingUser);
        
        await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => 
            _handler.Handle(command, CancellationToken.None));
        
        _mockRepo.Verify(r => r.User.CreateUser(It.IsAny<User>()), Times.Never);
        _mockEmail.Verify(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}