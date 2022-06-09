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

    public ICollection<User> Users { get; set; }

    public static string Default => User;
    public const string User = "user";
    public const string Admin = "admin";
}