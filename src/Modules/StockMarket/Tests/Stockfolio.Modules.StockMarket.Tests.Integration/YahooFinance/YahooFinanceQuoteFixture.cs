using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Shared.Infrastructure.Serialization;

namespace Stockfolio.Modules.StockMarket.Tests.Contracts.YahooFinance;

public class YahooFinanceQuoteFixture : IDisposable
{
    public IHttpClientFactory HttpClientFactory { get; private set; }
    public IJsonSerializer JsonSerializer { get; private set; }
    public string YahooFinanceBaseUrl { get; private set; }
    private readonly ServiceProvider _serviceProvider;

    public YahooFinanceQuoteFixture()
    {
        var configuration = new ConfigurationBuilder()
                           .AddJsonFile("module.stockmarket.json")
                           .AddEnvironmentVariables()
                           .Build();
        YahooFinanceBaseUrl = configuration.GetValue<string>("stockmarket:financeYahooOptions:baseApiUrl");

        _serviceProvider = new ServiceCollection()
                                .AddHttpClient()
                                .AddSingleton<IJsonSerializer, SystemTextJsonSerializer>()
                                .BuildServiceProvider();

        HttpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
        JsonSerializer = _serviceProvider.GetRequiredService<IJsonSerializer>();
    }

    public void Dispose()
    {
        _serviceProvider?.Dispose();
    }
}