using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Modules.Portfolios.Api.Exceptions;
using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionToResponseMapper, AssetsModuleExceptionsToResponseMapper>();
        return services;
    }
}