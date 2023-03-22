using Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models.HistoricalData;
using System.Text.Json.Serialization;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal class YahooFinanceCurrentTradingPeriod
    {
        [JsonPropertyName("pre")]
        public YahooFinanceTimePeriod PreMarket { get; set; }

        public YahooFinanceTimePeriod Regular { get; set; }

        [JsonPropertyName("post")]
        public YahooFinanceTimePeriod PostMarket { get; set; }
    }