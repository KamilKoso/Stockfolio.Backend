using Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Core.Entities;

internal class Role : IdentityRole<Guid>
{
    public Role(string roleName) : base(roleName)
    {
        Id = Guid.NewGuid();
        NormalizedName = roleName.ToUpperInvariant();
    }

    private Role() : base()
    {
    }

    public IReadOnlyCollection<UserRole> UserRoles { get; init; }

    public static string Default => User;
    public const string User = "user";
    public const string Admin = "admin";
}