using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Users.Core.Queries;

internal class BrowseUsers : PagedQuery<UserDto>
{
    public string Email { get; init; }
    public string Role { get; init; }
    public string State { get; init; }
}