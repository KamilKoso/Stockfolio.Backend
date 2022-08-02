using Stockfolio.Modules.StockMarket.Application.DTO;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.StockMarket.Application.Queries;

internal class SearchQuotes : IQuery<SearchQuotesDto>
{
    private int _count = 6;

    public string Query { get; set; }

    public int Count
    {
        get => _count;
        set => _count = value switch
        {
            <= 0 => 6,
            _ => value
        };
    }
}