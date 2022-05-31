using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Modules.Users.Core;
using Stockfolio.Shared.Abstractions.Modules;
using Stockfolio.Shared.Infrastructure.Modules;

namespace Stockfolio.Modules.Users.Api;

internal class UsersModule : IModule
{
    public string Name { get; } = "Users";

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequests();
    }
}