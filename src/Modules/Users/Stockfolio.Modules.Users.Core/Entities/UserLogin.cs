using Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Core.Entities;

internal class UserLogin : IdentityUserLogin<Guid>
{
    public User User { get; init; }
}