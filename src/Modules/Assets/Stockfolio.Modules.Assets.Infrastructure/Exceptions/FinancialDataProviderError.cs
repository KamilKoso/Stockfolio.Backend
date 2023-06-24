using Stockfolio.Shared.Abstractions.Exceptions;
using System.Net;

namespace Stockfolio.Modules.Assets.Infrastructure.Exceptions;

internal class FinancialDataProviderError : StockfolioException
{
    public HttpStatusCode StatusCode { get; }

    public FinancialDataProviderError(string url, HttpStatusCode statusCode, string responseBody) :
        base($"Request to {url} failed with the status code {statusCode}, and response body: '{responseBody}'")
    {
        StatusCode = statusCode;
    }
}