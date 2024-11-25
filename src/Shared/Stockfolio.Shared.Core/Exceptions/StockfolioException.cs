﻿namespace Stockfolio.Shared.Core.Exceptions;

public abstract class StockfolioException : Exception
{
    protected StockfolioException(string message, string code = null) : base(message)
    {
        Code = (code ?? GetErrorCode()).ToSnakeCase();
    }

    public string Code { get; init; }

    private string GetErrorCode()
       => GetType()
          .Name
          .Replace("Exception", string.Empty);
}