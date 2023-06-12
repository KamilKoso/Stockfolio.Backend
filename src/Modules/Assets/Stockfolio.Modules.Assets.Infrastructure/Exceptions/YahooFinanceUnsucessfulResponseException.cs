using Stockfolio.Shared.Abstractions.Exceptions;
using System.Net;

namespace Stockfolio.Modules.Assets.Infrastructure.Exceptions;

internal class YahooFinanceUnsucessfullResponseException : StockfolioException
{
    public string Url { get; }
    public HttpStatusCode StatusCode { get; }
    public string ResponseBody { get; }

    public YahooFinanceUnsucessfullResponseException(string url, HttpStatusCode statusCode, string responseBody) :
        base($"Yahoo finance requst to {url} failed with the status code {statusCode}, and response body: '{responseBody}'")
    {
        Url = url;
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }
}