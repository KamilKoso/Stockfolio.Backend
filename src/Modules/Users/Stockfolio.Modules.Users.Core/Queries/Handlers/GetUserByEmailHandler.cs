using Microsoft.EntityFrameworkCore;
using Stockfolio.Modules.Users.Core.DAL;
using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Modules.Users.Core.Mappings;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Users.Core.Queries.Handlers;

internal sealed class GetUserByEmailHandler : IQueryHandler<GetUserByEmail, UserDto>
{
    private readonly UsersDbContext _dbContext;

    public GetUserByEmailHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> HandleAsync(GetUserByEmail query, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.User)
            .SingleOrDefaultAsync(x => x.Email == query.Email, cancellationToken);

        return user?.AsDto();
    }
}