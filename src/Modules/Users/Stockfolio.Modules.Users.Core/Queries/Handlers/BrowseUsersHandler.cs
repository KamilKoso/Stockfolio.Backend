﻿using Microsoft.EntityFrameworkCore;
using Stockfolio.Modules.Users.Core.Data;
using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Modules.Users.Core.Mappings;
using Stockfolio.Shared.Abstractions.Queries;
using Stockfolio.Shared.Infrastructure.Postgres;

namespace Stockfolio.Modules.Users.Core.Queries.Handlers;

internal sealed class BrowseUsersHandler : IQueryHandler<BrowseUsers, Paged<UserDto>>
{
    private readonly UsersDbContext _dbContext;

    public BrowseUsersHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Paged<UserDto>> HandleAsync(BrowseUsers query, CancellationToken cancellationToken = default)
    {
        var users = _dbContext.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.Email))
        {
            users = users.Where(x => x.Email == query.Email);
        }

        if (!string.IsNullOrWhiteSpace(query.Role))
        {
            users = users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                .Where(x => x.UserRoles.Select(x => x.Role.Name).Contains(query.Role));
        }

        if (!string.IsNullOrWhiteSpace(query.State) && Enum.TryParse<UserState>(query.State, true, out var state))
        {
            users = users.Where(x => x.State == state);
        }

        return users.AsNoTracking()
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.User)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => x.AsDto())
            .PaginateAsync(query, cancellationToken);
    }
}