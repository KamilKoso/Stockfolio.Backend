using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Infrastructure.Exceptions;

internal class InfrastructureExceptionsToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
    {
        return exception switch
        {
            FinancialDataProviderError ex => new ExceptionResponse(new ErrorsResponse(new Error(ex.Code, "Financial data provider is not available at the moment. Please try again later.")), ex.StatusCode),
            _ => null
        };
    }
}