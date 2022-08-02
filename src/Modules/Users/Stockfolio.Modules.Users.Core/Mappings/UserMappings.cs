using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Modules.Users.Core.Entities;

namespace Stockfolio.Modules.Users.Core.Mappings;

internal static class UserMappings
{
    private static readonly Dictionary<UserState, string> States = new()
    {
        [UserState.Active] = UserState.Active.ToString().ToLowerInvariant(),
        [UserState.Locked] = UserState.Locked.ToString().ToLowerInvariant()
    };

    public static UserDto AsDto(this User user)
        => new()
        {
            Id = user.Id,
            Email = user.Email,
            State = States[user.State],
            Roles = user.UserRoles.Select(x => x.Role.Name),
            CreatedAt = user.CreatedAt,
            EmailConfirmed = user.EmailConfirmed
        };
}