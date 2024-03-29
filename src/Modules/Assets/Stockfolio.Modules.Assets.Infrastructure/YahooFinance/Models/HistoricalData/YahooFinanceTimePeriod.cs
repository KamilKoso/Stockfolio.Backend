﻿using System.Text.Json.Serialization;

namespace Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models.HistoricalData;

internal class YahooFinanceTimePeriod
{
    public string Timezone { get; set; }

    [JsonPropertyName("start")]
    public long StartMiliseconds { get; set; }

    [JsonPropertyName("end")]
    public long EndMiliseconds { get; set; }

    public int GmtOffset { get; set; }
}