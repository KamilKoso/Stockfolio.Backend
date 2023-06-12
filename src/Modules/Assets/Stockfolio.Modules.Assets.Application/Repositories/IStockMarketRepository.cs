using Stockfolio.Modules.Assets.Application.Dto.Assets;

namespace Stockfolio.Modules.Assets.Application.Repositories;

internal interface IStockMarketRepository
{
    Task<SearchAssetsResultDto> SearchSecurities(string searchQuery, int count, CancellationToken cancellationToken = default);

    Task<IEnumerable<AssetDto>> GetSecurities(IEnumerable<string> symbols, CancellationToken cancellationToken = default);

    Task<AssetDto> GetSecurity(string symbol, CancellationToken cancellationToken = default);

    Task<HistoricalQuotesDto> GetHistoricalQuotes(string symbol, DateTimeOffset start, DateTimeOffset end, string interval = "1d", CancellationToken cancellationToken = default);

    Task<HistoricalQuotesDto> GetHistoricalQuotes(string symbol, string range, string interval = "1d", CancellationToken cancellationToken = default);
}