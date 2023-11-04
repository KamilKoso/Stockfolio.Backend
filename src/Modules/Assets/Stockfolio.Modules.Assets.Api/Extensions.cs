using Microsoft.Extensions.DependencyInjection;

namespace Stockfolio.Modules.Assets.Api;

internal static class Extensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        return services;
    }
}