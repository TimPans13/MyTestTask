using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using MyTestTask.Models;
using MyTestTask.Services.Implementations;
using MyTestTask.Services.Interfaces;
using Xunit;

public class AuthServiceTests
{
    [Fact]
    public async Task Login_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var userManager = new Mock<UserManager<IdentityUser>>(MockBehavior.Strict);
        userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new IdentityUser { Id = "123", UserName = "TestUser" });
        userManager.Setup(u => u.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        var signInManager = new Mock<SignInManager<IdentityUser>>(userManager.Object, null, null, null, null, null, null);
        var configuration = new Mock<IConfiguration>();
        configuration.Setup(c => c.GetSection(It.IsAny<string>()).Value)
            .Returns("your-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-key"); // Provide the actual security key

        var authService = new AuthService(signInManager.Object, userManager.Object, configuration.Object);

        // Act
        var loginModel = new LoginViewModel { Email = "test@example.com", Password = "TestPassword123!" };
        var token = await authService.Login(loginModel);

        // Assert
        Assert.NotNull(token);
    }

    [Fact]
    public async Task Register_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var userManager = new Mock<UserManager<IdentityUser>>(MockBehavior.Strict);
        userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new IdentityUser { Id = "123", UserName = "TestUser" });
        userManager.Setup(u => u.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        var signInManager = new Mock<SignInManager<IdentityUser>>(userManager.Object, null, null, null, null, null, null);
        var configuration = new Mock<IConfiguration>();
        configuration.Setup(c => c.GetSection(It.IsAny<string>()).Value)
            .Returns("your-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-key"); // Provide the actual security key

        var authService = new AuthService(signInManager.Object, userManager.Object, configuration.Object);

        // Act
        var registerModel = new RegisterViewModel { Email = "test@example.com", Password = "TestPassword123!" };
        var token = await authService.Register(registerModel);

        // Assert
        Assert.NotNull(token);
    }

    [Fact]
    public async Task Logout_ValidCall_SignsOut()
    {
        // Arrange
        var signInManager = new Mock<SignInManager<IdentityUser>>(MockBehavior.Strict);
        signInManager.Setup(s => s.SignOutAsync());

        var userManager = new Mock<UserManager<IdentityUser>>(MockBehavior.Strict);
        var configuration = new Mock<IConfiguration>();

        var authService = new AuthService(signInManager.Object, userManager.Object, configuration.Object);

        // Act
        await authService.Logout();

        // Assert
        signInManager.Verify(s => s.SignOutAsync(), Times.Once);
    }
}
