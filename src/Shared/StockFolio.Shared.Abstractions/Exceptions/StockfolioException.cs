using System;

namespace Stockfolio.Shared.Abstractions.Exceptions;

public abstract class StockfolioException : Exception
{
    protected StockfolioException(string message) : base(message)
    {
    }
}