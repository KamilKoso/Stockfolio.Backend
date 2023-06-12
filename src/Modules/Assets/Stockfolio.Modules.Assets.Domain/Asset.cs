using Stockfolio.Modules.Assets.Core.Entities;
using Stockfolio.Modules.Assets.Core.Exceptions;
using Stockfolio.Modules.Assets.Core.ValueObjects;
using Stockfolio.Shared.Abstractions.Kernel.Types;
using Stockfolio.Shared.Abstractions.Kernel.ValueObjects.Currencies;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Stockfolio.Modules.Assets.Application")]

namespace Stockfolio.Modules.Assets.Core;

internal class Asset
{
    private List<Quote> _historicalQuotes = new();

    public AssetId Id { get; init; }
    public UserId Owner { get; init; }
    public AssetName Name { get; private set; }
    public Currency Currency { get; private set; }

    public IReadOnlyCollection<Quote> HistoricalQuotes => _historicalQuotes;

    public Asset(AssetName assetName, UserId assetOwner, Currency currency)
    {
        Id = new();
        Name = assetName;
        Owner = assetOwner;
        Currency = currency;
    }

    public void ChangeName(AssetName name)
    {
        Name = name;
    }

    public void ChangeCurrency(Currency newCurrency, IDictionary<QuoteId, ExchangeRate> exchangeRates)
    {
        if (exchangeRates.Count > _historicalQuotes.Count)
        {
            throw new DuplicatedExchangeRatesForTheQuoteException();
        }

        foreach (var quote in HistoricalQuotes)
        {
            var isExchangeRatePresent = exchangeRates.TryGetValue(quote.Id, out var exchangeRate);
            if (!isExchangeRatePresent)
            {
                throw new MissingExchangeRateForTheQuoteException(quote.Id);
            }

            quote.Value *= exchangeRate;
        }

        Currency = newCurrency;
    }

    public void AddQuotes(params Quote[] quotes)
    {
        _historicalQuotes.AddRange(quotes);
    }

    public void ModifyQuote(Quote modifiedQuote)
    {
        var quote = _historicalQuotes.SingleOrDefault(x => x.Id == modifiedQuote.Id);
        if (quote is null)
        {
            throw new QuoteNotFoundException(modifiedQuote.Id, Name);
        }
        quote = modifiedQuote;
    }
}