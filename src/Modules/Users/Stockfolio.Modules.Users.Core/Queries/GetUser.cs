using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Users.Core.Queries;

internal class GetUser : IQuery<UserDto>
{
    public Guid UserId { get; init; }
}