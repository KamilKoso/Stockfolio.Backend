using Moq;
using Shouldly;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Modules.Users.Core.Options;
using Stockfolio.Modules.Users.Core.Validators;
using Identity = Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Tests.Unit.Validators;

public class UserValidatorTests
{
    private readonly UserErrorDescriber _userErrorDescriber = new();
    private readonly string _invalidEmailProvider = "IamInvalidEmailProvider.com";
    private readonly string _validEmailProvider = "IamValidEmailProvider.com";

    [Fact]
    public async Task ValidateAsync_WhenPassedInvalidEmailProvider_ValidationFails()
    {
        // Arrange
        var expectedErrorMessage = new Identity.IdentityError()
        {
            Code = nameof(_userErrorDescriber.InvalidEmailProvider),
            Description = $"Invalid email provider. '{_invalidEmailProvider}' is not accepted email provider."
        };

        var user = new User()
        {
            UserName = "Username",
            Email = $"John@{_invalidEmailProvider}"
        };

        var userManagerMock = GetUserManagerMock(userManagerMock =>
        {
            userManagerMock
                .Setup(x => x.GetUserNameAsync(user))
                .ReturnsAsync(user.UserName);
        });
        var identityOptions = new IdentityOptions()
        {
            InvalidEmailProviders = new List<string>() { _invalidEmailProvider }
        };

        var userValidator = GetUserValidator(identityOptions);

        // Act
        var result = await userValidator.ValidateAsync(userManagerMock, user);

        // Assert
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeFalse();
        result.Errors.Count().ShouldBe(1);
        var error = result.Errors.First();
        error.Code.ShouldBe(expectedErrorMessage.Code);
        error.Description.ShouldBe(expectedErrorMessage.Description);
    }

    [Fact]
    public async Task ValidateAsync_WhenPassedValidEmailProvider_ValidationPasses()
    {
        // Arrange
        var user = new User()
        {
            UserName = "Username",
            Email = $"John@{_validEmailProvider}"
        };

        var userManagerMock = GetUserManagerMock(userManagerMock =>
        {
            userManagerMock
                .Setup(x => x.GetUserNameAsync(user))
                .ReturnsAsync(user.UserName);
        });
        var identityOptions = new IdentityOptions()
        {
            InvalidEmailProviders = new List<string>() { _invalidEmailProvider }
        };

        var userValidator = GetUserValidator(identityOptions);

        // Act
        var result = await userValidator.ValidateAsync(userManagerMock, user);

        // Assert
        result.ShouldNotBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    private UserValidator GetUserValidator(IdentityOptions identityOptions)
        => new(identityOptions, _userErrorDescriber);

    private Identity.UserManager<User> GetUserManagerMock(Action<Mock<Identity.UserManager<User>>>? setups = null)
    {
        var userManagerMock = new Mock<Identity.UserManager<User>>(Mock.Of<Identity.IUserStore<User>>(), null, null, null, null, null, null, null, null);
        setups?.Invoke(userManagerMock);
        return userManagerMock.Object;
    }
}