using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.StockMarket.Application.Queries;

internal class GetHistoricalData : IQuery<HistoricalDataDto>
{
    public GetHistoricalData(string symbol, DateTimeOffset? start, DateTimeOffset? end, string range, string interval)
    {
        Symbol = symbol;
        Range = range;
        Interval = interval ?? "1d";
        Start = start ?? DateTimeOffset.Now.AddYears(-1);
        End = end ?? DateTimeOffset.Now;
    }

    public string Symbol { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public string Interval { get; set; }
    public string Range { get; set; }
}