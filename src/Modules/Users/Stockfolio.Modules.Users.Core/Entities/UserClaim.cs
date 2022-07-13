using Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Core.Entities;

internal class UserClaim : IdentityUserClaim<Guid>
{
    public User User { get; init; }
}