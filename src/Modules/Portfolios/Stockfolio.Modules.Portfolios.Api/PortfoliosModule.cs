using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Modules.Portfolios.Application;
using Stockfolio.Modules.Portfolios.Core;
using Stockfolio.Modules.Portfolios.Infrastructure;
using Stockfolio.Shared.Abstractions.Modules;
using Stockfolio.Shared.Infrastructure.Contracts;

namespace Stockfolio.Modules.Wallets.Api;

internal class PortfoliosModule : IModule
{
    public string Name { get; } = "Portfolios";

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore();
        services.AddApplication();
        services.AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseContracts();
    }
}