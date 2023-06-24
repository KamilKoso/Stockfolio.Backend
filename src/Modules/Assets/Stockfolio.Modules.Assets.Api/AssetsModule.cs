using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Modules.Assets.Application;
using Stockfolio.Modules.Assets.Infrastructure;
using Stockfolio.Shared.Abstractions.Modules;
using Stockfolio.Shared.Infrastructure.Modules;

namespace Stockfolio.Modules.Portfolios.Api;

internal class AssetsModule : IModule
{
    public string Name { get; } = "Assets";

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);
        services.AddApi();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequests();
    }
}