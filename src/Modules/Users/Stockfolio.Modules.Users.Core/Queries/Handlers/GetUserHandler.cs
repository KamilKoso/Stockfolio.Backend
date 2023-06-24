using Microsoft.EntityFrameworkCore;
using Stockfolio.Modules.Users.Core.Data;
using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Modules.Users.Core.Mappings;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Users.Core.Queries.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly UsersDbContext _dbContext;

    public GetUserHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id == query.UserId, cancellationToken);

        return user?.AsDto();
    }
}