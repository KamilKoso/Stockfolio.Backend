using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Modules.Assets.Application.Queries.Assets;
using Stockfolio.Modules.Assets.Application.Queries.Assets.Handlers;
using Stockfolio.Shared.Infrastructure.Queries;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Stockfolio.Modules.Assets.Api")]
[assembly: InternalsVisibleTo("Stockfolio.Modules.Assets.Infrastructure")]

namespace Stockfolio.Modules.Assets.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCachedQueryDecorator<GetAssets, IEnumerable<AssetDto>>(GetAssetsHandler.CacheKeyBuilder, GetAssetsHandler.CacheExpiration);
        services.AddCachedQueryDecorator<GetHistoricalQuotes, HistoricalQuotesDto>(GetHistoricalQuotesHandler.CacheKeyBuilder, GetHistoricalQuotesHandler.CacheExpirationBuilder);
        return services;
    }
}