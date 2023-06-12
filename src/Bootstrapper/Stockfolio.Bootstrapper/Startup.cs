using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stockfolio.Shared.Abstractions.Modules;
using Stockfolio.Shared.Infrastructure;
using Stockfolio.Shared.Infrastructure.Contracts;
using Stockfolio.Shared.Infrastructure.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Stockfolio.Bootstrapper;

public class Startup
{
    private readonly IList<Assembly> _assemblies;
    private readonly IList<IModule> _modules;
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _assemblies = ModuleLoader.LoadAssemblies(configuration, "Stockfolio.Modules.");
        _modules = ModuleLoader.LoadModules(_assemblies);
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddModularInfrastructure(_assemblies, _modules, _configuration);
        foreach (var module in _modules)
        {
            module.Register(services, _configuration);
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        logger.LogInformation("Modules: {modules}", string.Join(", ", _modules.Select(x => x.Name)));
        app.UseModularInfrastructure();
        foreach (var module in _modules)
        {
            module.Use(app);
        }

        app.ValidateContracts(_assemblies);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context => context.Response.WriteAsync("Stockfolio API"));
            endpoints.MapModuleInfo();
        });

        _assemblies.Clear();
        _modules.Clear();
    }
}