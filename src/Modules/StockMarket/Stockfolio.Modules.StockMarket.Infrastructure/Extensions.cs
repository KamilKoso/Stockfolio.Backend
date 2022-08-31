using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Modules.StockMarket.Application.Repositories;
using Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Options;
using Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Repositories;
using Stockfolio.Shared.Infrastructure;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Stockfolio.Modules.StockMarket.Tests.Integration")]
[assembly: InternalsVisibleTo("Stockfolio.Modules.StockMarket.Api")]

namespace Stockfolio.Modules.StockMarket.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var financeYahooOptions = services.GetOptions<YahooFinanceOptions>("stockmarket:financeYahooOptions");
        services.AddSingleton(financeYahooOptions);

        services.AddScoped<IQuotesRepository, YahooFinanceQuotesRepository>();
        return services;
    }
}