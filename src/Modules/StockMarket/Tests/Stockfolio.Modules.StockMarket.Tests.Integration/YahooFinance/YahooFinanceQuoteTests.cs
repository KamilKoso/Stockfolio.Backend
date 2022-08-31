using Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;
using System.Text.Json;
using System.Text.Json.JsonDiffPatch.Xunit;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Stockfolio.Modules.StockMarket.Tests.Contracts.YahooFinance;

public class YahooFinanceQuoteTests : IClassFixture<YahooFinanceQuoteFixture>
{
    private readonly YahooFinanceQuoteFixture _fixture;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public YahooFinanceQuoteTests(YahooFinanceQuoteFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [InlineData("KGH.WA")]
    [InlineData("AAPL")]
    [InlineData("MSFT")]
    [InlineData("TSLA")]
    public async Task GivenStockSymbol_CanDeserializeCorrectly(string symbol)
    {
        symbol = "KGH.WA";

        // Arrange
        var requestUrl = $"{_fixture.YahooFinanceBaseUrl}/v7/finance/quote?symbols={symbol}";
        var httpClient = _fixture.HttpClientFactory.CreateClient();

        // Act
        var response = await httpClient.GetAsync(requestUrl);
        var responseStr = await response.Content.ReadAsStringAsync();
        var original = JsonNode.Parse(responseStr)["quoteResponse"]["result"][0].ToString();
        var deserialized = _fixture.JsonSerializer.Deserialize<YahooFinanceQuoteDetails>(original);
        var actual = JsonSerializer.Serialize(deserialized, _jsonSerializerOptions);

        // Assert
        response.EnsureSuccessStatusCode();
        JsonAssert.Equal(original, actual);
    }
}