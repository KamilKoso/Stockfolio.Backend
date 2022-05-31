using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Modules.Users.Core.DAL;
using Stockfolio.Modules.Users.Core.DAL.Repositories;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Modules.Users.Core.Options;
using Stockfolio.Modules.Users.Core.Repositories;
using Stockfolio.Modules.Users.Core.Services;
using Stockfolio.Shared.Infrastructure;
using Stockfolio.Shared.Infrastructure.Messaging.Outbox;
using Stockfolio.Shared.Infrastructure.Postgres;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Stockfolio.Modules.Users.Api")]
[assembly: InternalsVisibleTo("Stockfolio.Modules.Users.Tests.Unit")]

namespace Stockfolio.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var registrationOptions = services.GetOptions<RegistrationOptions>("users:registration");
        services.AddSingleton(registrationOptions);

        var passwordPolicyOptions = services.GetOptions<PasswordStrengthPolicyOptions>("users:passwordStrengthPolicy");
        services.AddSingleton(passwordPolicyOptions);

        return services
            .AddSingleton<IUserRequestStorage, UserRequestStorage>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddPostgres<UsersDbContext>()
            .AddOutbox<UsersDbContext>()
            .AddUnitOfWork<UsersUnitOfWork>()
            .AddInitializer<UsersInitializer>();
    }
}