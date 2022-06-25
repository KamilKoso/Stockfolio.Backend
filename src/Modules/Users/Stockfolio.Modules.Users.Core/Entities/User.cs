using Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Core.Entities;

internal class User : IdentityUser<Guid>
{
    public IReadOnlyCollection<UserRole> UserRoles { get; init; }
    public UserState State { get; init; }
    public DateTime CreatedAt { get; init; }
}