using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Users.Core.Queries;

internal class GetUser : IQuery<UserDetailsDto>
{
    public Guid UserId { get; init; }
}