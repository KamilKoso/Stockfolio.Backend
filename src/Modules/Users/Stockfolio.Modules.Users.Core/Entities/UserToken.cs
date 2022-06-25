using Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Core.Entities;

internal class UserToken : IdentityUserToken<Guid>
{
}