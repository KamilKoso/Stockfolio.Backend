using Stockfolio.Shared.Abstractions.Exceptions;
using StockFolio.Shared.Infrastructure;
using System;
using System.Net;

namespace Stockfolio.Shared.Infrastructure.Exceptions;

internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            StockfolioException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message))
                , HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(new ErrorsResponse(new Error("Error", "Unrecognized error occurred.")),
                HttpStatusCode.InternalServerError)
        };

    private record Error(string Code, string Message);

    private record ErrorsResponse(params Error[] Errors);

    private static string GetErrorCode(Exception exception)
        => exception.GetType()
                     .Name
                     .Replace("Exception", string.Empty)
                     .ToSnakeCase();
}