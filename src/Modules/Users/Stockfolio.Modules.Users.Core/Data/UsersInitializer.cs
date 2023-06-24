using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Shared.Infrastructure;

namespace Stockfolio.Modules.Users.Core.Data;

internal sealed class UsersInitializer : IInitializer
{
    private readonly UsersDbContext _dbContext;
    private readonly ILogger<UsersInitializer> _logger;

    public UsersInitializer(UsersDbContext dbContext, ILogger<UsersInitializer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InitAsync()
    {
        if (await _dbContext.Roles.AnyAsync())
        {
            return;
        }

        await AddRolesAsync();
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddRolesAsync()
    {
        await _dbContext.Roles.AddRangeAsync(
            new Role(Role.Admin),
            new Role(Role.User)
            );
        _logger.LogInformation("Initialized roles.");
    }
}