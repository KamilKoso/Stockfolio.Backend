using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Users.Core.Queries;

internal class GetUserByEmail : IQuery<UserDto>
{
    public string Email { get; init; }
}