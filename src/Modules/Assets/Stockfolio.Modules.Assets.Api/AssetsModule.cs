using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Modules.Assets.Infrastructure;
using Stockfolio.Modules.Portfolios.Application;
using Stockfolio.Shared.Abstractions.Modules;
using Stockfolio.Shared.Infrastructure.Modules;

namespace Stockfolio.Modules.Wallets.Api;

internal class AssetsModule : IModule
{
    public string Name { get; } = "Assets";

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequests();
    }
}