using Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Core.Entities;

internal class User : IdentityUser<Guid>
{
    public User()
    {
        UserRoles = new List<UserRole>();
        UserClaims = new List<UserClaim>();
        UserLogins = new List<UserLogin>();
    }

    public IReadOnlyCollection<UserRole> UserRoles { get; init; }
    public IReadOnlyCollection<UserClaim> UserClaims { get; init; }
    public IReadOnlyCollection<UserLogin> UserLogins { get; init; }
    public UserState State { get; init; }
    public DateTime CreatedAt { get; init; }
}