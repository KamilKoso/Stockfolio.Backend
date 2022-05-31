using Stockfolio.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace Stockfolio.Modules.Users.Core.Commands;
internal record SignUp([Required] string Password) : ICommand
{
    private string _email;

    public Guid UserId { get; init; } = Guid.NewGuid();

    [Required]
    [EmailAddress]
    public string Email
    {
        get { return _email; }
        set { _email = value.ToLowerInvariant(); }
    }
}