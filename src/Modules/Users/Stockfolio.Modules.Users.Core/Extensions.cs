﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Modules.Users.Core.Data;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Modules.Users.Core.Managers;
using Stockfolio.Modules.Users.Core.Stores;
using Stockfolio.Modules.Users.Core.Validators;
using Stockfolio.Shared.Infrastructure;
using Stockfolio.Shared.Infrastructure.Messaging.Outbox;
using Stockfolio.Shared.Infrastructure.Postgres;
using System.Runtime.CompilerServices;
using IdentityOptions = Stockfolio.Modules.Users.Core.Options.IdentityOptions;

[assembly: InternalsVisibleTo("Stockfolio.Modules.Users.Api")]
[assembly: InternalsVisibleTo("Stockfolio.Modules.Users.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] // XUnit internals

namespace Stockfolio.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPostgres<UsersDbContext>(configuration)
            .AddOutbox<UsersDbContext>(configuration)
            .AddUnitOfWork<UsersUnitOfWork>()
            .AddInitializer<UsersInitializer>()
            .AddIdentity(configuration);
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var identityOptions = configuration.GetOptions<IdentityOptions>("users:identity");
        services
            .AddSingleton(identityOptions)
            .AddIdentity<User, Role>(options =>
            {
                options.Password = identityOptions.PasswordOptions;
                options.SignIn = identityOptions.SignInOptions;
                options.User.RequireUniqueEmail = identityOptions.RequireUniqueEmail;
            })
            .AddErrorDescriber<UserErrorDescriber>()
            .AddEntityFrameworkStores<UsersDbContext>()
            .AddUserStore<UserStore>()
            .AddUserManager<UserManager>()
            .AddUserValidator<UserValidator>()
            .AddDefaultTokenProviders();
        return services;
    }
}