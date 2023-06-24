using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Modules.Assets.Infrastructure.Exceptions;
using Stockfolio.Modules.Assets.Infrastructure.Repositories;
using Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Options;
using Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Repositories;
using Stockfolio.Shared.Abstractions.Exceptions;
using Stockfolio.Shared.Infrastructure;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;

[assembly: InternalsVisibleTo("Stockfolio.Modules.Assets.Tests.Integration")]
[assembly: InternalsVisibleTo("Stockfolio.Modules.Assets.Api")]
namespace Stockfolio.Modules.Assets.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var optionsSection = "assets:yahooFinanceOptions";
        var yahooFinanceOptions = configuration.GetOptions<YahooFinanceOptions>(optionsSection);
        services.Configure<YahooFinanceOptions>(configuration.GetSection(optionsSection));

        services.AddScoped<IStockMarketRepository, YahooFinanceApi>();
        services.AddScoped<IAssetsRepository, AssetsRepository>();
        services.AddSingleton<IExceptionToResponseMapper, InfrastructureExceptionsToResponseMapper>();

        services.AddHttpClient<IStockMarketRepository, YahooFinanceApi>((options) =>
        {
            options.BaseAddress = new Uri(yahooFinanceOptions.BaseApiUrl);
        }).AddYahooFinanceSessionManagement()
          .AddTransientHttpErrorPolicy(policyBuilder =>
                                            policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5, fastFirst: true)));

        services.AddScoped<IForexRepository, YahooFinanceApi>();
        services.AddHttpClient<IForexRepository, YahooFinanceApi>((options) =>
        {
            options.BaseAddress = new Uri(yahooFinanceOptions.BaseApiUrl);
        }).AddYahooFinanceSessionManagement()
          .AddTransientHttpErrorPolicy(policyBuilder =>
                                            policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5, fastFirst: true)));

        return services;
    }

    private static IHttpClientBuilder AddYahooFinanceSessionManagement(this IHttpClientBuilder builder)
    {
        return builder.AddPolicyHandler((provider, request) =>
        {
            return Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                .RetryAsync(1, async (response, retryCount, context) =>
                {
                    var yahooFinanceApi = (YahooFinanceApi)provider.GetRequiredService<IStockMarketRepository>();
                    await yahooFinanceApi.RefreshTheSession();
                    UpdateRequestWithNewCrumb(request);
                });
        });

        static void UpdateRequestWithNewCrumb(HttpRequestMessage request)
        {
            var uriBuilder = new UriBuilder(request.RequestUri);
            var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParams["crumb"] = YahooFinanceApi._crumb;
            uriBuilder.Query = queryParams.ToString();
            request.RequestUri = uriBuilder.Uri;
        }
    }
}