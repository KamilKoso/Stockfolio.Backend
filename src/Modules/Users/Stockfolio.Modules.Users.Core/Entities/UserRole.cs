using Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Core.Entities;

internal class UserRole : IdentityUserRole<Guid>
{
    public UserRole(Guid userId, Guid roleId) : base()
    {
        RoleId = roleId;
        UserId = userId;
    }

    public UserRole() : base()
    {
    }

    public Role Role { get; private set; }

    public User User { get; private set; }
}