using Stockfolio.Modules.Assets.Infrastructure.Exceptions;
using Stockfolio.Shared.Abstractions.Exceptions;
using System;

namespace Stockfolio.Modules.Portfolios.Api.Exceptions;

internal class AssetsModuleExceptionsToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
    {
        return exception switch
        {
            YahooFinanceUnsucessfulResponseException ex => new ExceptionResponse(new ErrorsResponse(new Error(ex.Code, ex.Message)), ex.StatusCode),
            _ => null
        };
    }
}