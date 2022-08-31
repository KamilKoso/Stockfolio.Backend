using System.Text.Json.Serialization;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal class YahooFinanceTimePeriod
{
    public string Timezone { get; set; }

    [JsonPropertyName("start")]
    public long StartMiliseconds { get; set; }

    [JsonPropertyName("end")]
    public long EndMiliseconds { get; set; }

    public int GmtOffset { get; set; }
}