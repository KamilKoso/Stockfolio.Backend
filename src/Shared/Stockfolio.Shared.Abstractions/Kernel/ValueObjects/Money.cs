﻿using Stockfolio.Shared.Abstractions.Kernel.Exceptions;
using Stockfolio.Shared.Abstractions.Kernel.ValueObjects.Currencies;

namespace Stockfolio.Shared.Abstractions.Kernel.ValueObjects;

public record Money(decimal Amount, Currency Currency)
{
    public static Money operator *(Money money, uint times)
    {
        return money with { Amount = money.Amount * times };
    }

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new MoneyOperationsArePossibleOnlyForTheSameCurrencyException();
        }

        return left with { Amount = left.Amount + right.Amount };
    }

    public override string ToString() => $"{Amount} {Currency}";
}