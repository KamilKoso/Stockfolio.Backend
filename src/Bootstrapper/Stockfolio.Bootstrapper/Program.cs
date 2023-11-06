using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Stockfolio.Shared.Infrastructure.Logging;
using Stockfolio.Shared.Infrastructure.Modules;
using System.Threading.Tasks;

namespace Stockfolio.Bootstrapper;

public class Program
{
    public static Task Main(string[] args)
        => CreateHostBuilder(args).Build().RunAsync();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .AddEnviromentVariables()
            .ConfigureModules()
            .UseLogging();
}