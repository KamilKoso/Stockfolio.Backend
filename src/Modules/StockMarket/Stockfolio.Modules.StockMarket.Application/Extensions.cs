using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Stockfolio.Modules.StockMarket.Api")]
[assembly: InternalsVisibleTo("Stockfolio.Modules.StockMarket.Infrastructure")]

namespace Stockfolio.Modules.Portfolios.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}