using System.Text.Json.Serialization;

namespace Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models;

internal class SearchYahooFinanceSecurities : YahooFinanceQuoteBase
{
    public string Index { get; set; }
    public double Score { get; set; }

    [JsonPropertyName("exchDisp")]
    public string ExchangeDisplayName { get; set; }

    public string Industry { get; set; }
    public string Sector { get; set; }
    public bool? IsYahooFinance { get; set; }
    public bool? DispSecIndFlag { get; set; } // no idea what that is
}