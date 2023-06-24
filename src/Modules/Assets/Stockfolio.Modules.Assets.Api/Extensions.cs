using Microsoft.Extensions.DependencyInjection;

namespace Stockfolio.Modules.Portfolios.Api;

internal static class Extensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        return services;
    }
}