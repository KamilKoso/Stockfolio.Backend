using System.Collections.ObjectModel;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal class YahooFinanceEvents
{
    public IReadOnlyDictionary<long, YahooFinanceDividendDto> Dividends { get; set; }
    public IReadOnlyDictionary<long, YahooFinanceSplitDto> Splits { get; set; }
}