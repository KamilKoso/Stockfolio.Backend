namespace Stockfolio.Modules.Assets.Application.Dto.Forex;
internal record ExchangeRatesDto(string From, string To, IEnumerable<ExchangeRateDto> Rates);