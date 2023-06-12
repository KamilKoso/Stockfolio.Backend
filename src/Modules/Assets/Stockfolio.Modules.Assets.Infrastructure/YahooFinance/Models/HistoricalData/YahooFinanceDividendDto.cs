using System.Text.Json.Serialization;

namespace Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models;

internal class YahooFinanceDividendDto
{
    public decimal Amount { get; set; }

    [JsonPropertyName("date")]
    public long DateInSeconds { get; set; }
}