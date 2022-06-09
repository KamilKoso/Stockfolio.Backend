using Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Core.Entities;

internal class User : IdentityUser<Guid>
{
    public ICollection<Role> Roles { get; set; }
    public UserState State { get; set; }
    public DateTime CreatedAt { get; set; }
}