using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Stockfolio.Modules.Assets.Api")]
[assembly: InternalsVisibleTo("Stockfolio.Modules.Assets.Infrastructure")]

namespace Stockfolio.Modules.Portfolios.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}