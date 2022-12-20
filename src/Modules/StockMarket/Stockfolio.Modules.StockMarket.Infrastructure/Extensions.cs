using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
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
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var financeYahooOptions = configuration.GetOptions<YahooFinanceOptions>("stockmarket:financeYahooOptions");
        services.AddScoped<IQuotesRepository, YahooFinanceApi>();
        services.AddHttpClient<IQuotesRepository, YahooFinanceApi>((options) =>
        {
            options.BaseAddress = new Uri(financeYahooOptions.BaseApiUrl);
        }).AddTransientHttpErrorPolicy(policyBuilder =>
                                            policyBuilder.WaitAndRetryAsync(
                                                    Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5, fastFirst: true)));
        return services;
    }
}