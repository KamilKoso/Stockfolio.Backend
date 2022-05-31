using Stockfolio.Modules.Users.Core.Exceptions;
using Stockfolio.Modules.Users.Core.Options;

namespace Stockfolio.Modules.Users.Tests.Unit;

public class PasswordPolicyOptionsTests
{
    [Theory]
    [InlineData("123")]
    [InlineData(" ")]
    [InlineData("!@#$%^&*()")]
    [InlineData("abc")]
    [InlineData("this_is_very_long_password_i_hope_it_will_pass_the_test")]
    public void GivenEmptyPolicy_ShouldPassEverythingExceptNullsOrEmptyPasswords(string testPassword)
    {
        // Arrange
        var options = new PasswordStrengthPolicyOptions();

        // Act
        var exception = Record.Exception(() => options.Validate(testPassword));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void GivenTooLongPassword_ShouldThrowException()
    {
        // Arrange
        var options = new PasswordStrengthPolicyOptions()
        {
            MaxLength = 2
        };
        string testPassword = "123";

        // Act
        var exception = Record.Exception(() => options.Validate(testPassword));

        //Assert
        Assert.True(testPassword.Length > options.MaxLength);
        Assert.IsType<InvalidPasswordException>(exception);
    }

    [Fact]
    public void GivenTooShortPassword_ShouldThrowException()
    {
        // Arrange
        var options = new PasswordStrengthPolicyOptions()
        {
            MinLength = 4
        };
        string testPassword = "123";

        // Act
        var exception = Record.Exception(() => options.Validate(testPassword));

        //Assert
        Assert.True(testPassword.Length < options.MinLength);
        Assert.IsType<InvalidPasswordException>(exception);
    }

    [Theory]
    [InlineData("1", 1, 1)]
    [InlineData("12", 1, 2)]
    [InlineData("123", 2, 8)]
    public void GivenPasswordThatFitsLengthConstraints_ShouldNotThrowException(string testPassword, int minLength, int maxLength)
    {
        // Arrange
        var options = new PasswordStrengthPolicyOptions()
        {
            MinLength = minLength,
            MaxLength = maxLength
        };

        // Act
        var exception = Record.Exception(() => options.Validate(testPassword));

        //Assert
        Assert.True(testPassword.Length >= options.MinLength && testPassword.Length <= options.MaxLength);
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("IAmLackingSpecialCharacter")]
    public void GivenPasswordLackingSpecialCharacter_WhenItsRequired_ShouldThrowException(string testPassword)
    {
        // Arrange
        var options = new PasswordStrengthPolicyOptions()
        {
            RequireSpecialCharacter = true
        };

        // Act
        var exception = Record.Exception(() => options.Validate(testPassword));

        //Assert
        Assert.IsType<InvalidPasswordException>(exception);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("!")]
    [InlineData("@")]
    [InlineData("#")]
    [InlineData("%")]
    [InlineData("^")]
    [InlineData("*")]
    [InlineData("(")]
    [InlineData(")")]
    [InlineData("[")]
    [InlineData("]")]
    [InlineData("{")]
    [InlineData("}")]
    [InlineData(";")]
    [InlineData(":")]
    [InlineData("'")]
    [InlineData("\"")]
    [InlineData("\\")]
    [InlineData("|")]
    [InlineData(".")]
    [InlineData(",")]
    [InlineData("`")]
    [InlineData("~")]
    [InlineData("IAmNotLackingSpecialCharacter!!!!")]
    public void GivenPasswordContainingSpecialCharacter_WhenItsRequired_ShouldNotThrowException(string testPassword)
    {
        // Arrange
        var options = new PasswordStrengthPolicyOptions()
        {
            RequireSpecialCharacter = true
        };

        // Act
        var exception = Record.Exception(() => options.Validate(testPassword));

        //Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("!@#")]
    [InlineData("IAmLackingNumber")]
    public void GivenPasswordLackingNumber_WhenItsRequired_ShouldThrowException(string testPassword)
    {
        // Arrange
        var options = new PasswordStrengthPolicyOptions()
        {
            RequireNumber = true
        };

        // Act
        var exception = Record.Exception(() => options.Validate(testPassword));

        //Assert
        Assert.IsType<InvalidPasswordException>(exception);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("123")]
    [InlineData("123@#@#@#@")]
    [InlineData("IAmNotLackingNumer - 123")]
    public void GivenPasswordContainingNumber_WhenItsRequired_ShouldNotThrowException(string testPassword)
    {
        // Arrange
        var options = new PasswordStrengthPolicyOptions()
        {
            RequireNumber = true
        };

        // Act
        var exception = Record.Exception(() => options.Validate(testPassword));

        //Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("!@#")]
    [InlineData("iamlackinguppercasecharacter")]
    public void GivenPasswordLackingUppercaseCharacter_WhenItsRequired_ShouldThrowException(string testPassword)
    {
        // Arrange
        var options = new PasswordStrengthPolicyOptions()
        {
            RequireUppercaseCharacter = true
        };

        // Act
        var exception = Record.Exception(() => options.Validate(testPassword));

        //Assert
        Assert.IsType<InvalidPasswordException>(exception);
    }

    [Theory]
    [InlineData("AAAA")]
    [InlineData("AAAA123")]
    [InlineData("AAAA!@#")]
    [InlineData("IAmNotLackingUppercaseCharacter")]
    public void GivenPasswordContainingUppercaseCharacter_WhenItsRequired_ShouldNotThrowException(string testPassword)
    {
        // Arrange
        var options = new PasswordStrengthPolicyOptions()
        {
            RequireUppercaseCharacter = true
        };

        // Act
        var exception = Record.Exception(() => options.Validate(testPassword));

        //Assert
        Assert.Null(exception);
    }
}