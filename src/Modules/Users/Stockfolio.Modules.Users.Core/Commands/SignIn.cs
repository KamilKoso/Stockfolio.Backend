using Stockfolio.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace Stockfolio.Modules.Users.Core.Commands;
internal record SignIn([Required] string Password) : ICommand
{
    private string _email;

    [Required]
    [EmailAddress]
    public string Email
    {
        get { return _email; }
        set { _email = value.ToLowerInvariant(); }
    }

    public Guid Id { get; init; } = Guid.NewGuid();
}