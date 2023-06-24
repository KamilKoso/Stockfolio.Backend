using Serilog.Core;
using Serilog.Events;
using Stockfolio.Shared.Infrastructure.Contexts;
using System.Collections.Generic;

namespace Stockfolio.Shared.Infrastructure.Logging.Enrichers;

internal class DiagnosticEnricher : ILogEventEnricher
{
    private static readonly string CorrelationIdKey = "CorrelationId";
    private static readonly string RequestIdKey = "RequestId";
    private static readonly string TraceIdKey = "TraceId";
    private static readonly string UserIdKey = "UserId";
    private readonly ContextAccessor _contextAccessor;

    public DiagnosticEnricher() : this(new ContextAccessor())
    {
    }

    private DiagnosticEnricher(ContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var propertiesToAddOrUpdate = new Dictionary<string, object>
        {
            { CorrelationIdKey, _contextAccessor.Context?.CorrelationId },
            { RequestIdKey, _contextAccessor.Context?.RequestId },
            { TraceIdKey, _contextAccessor.Context?.TraceId },
            { UserIdKey, _contextAccessor.Context?.Identity.Id }
        };

        foreach (var kvp in propertiesToAddOrUpdate)
        {
            if (!logEvent.Properties.ContainsKey(kvp.Key) && kvp.Value is not null)
            {
                var property = new LogEventProperty(kvp.Key, new ScalarValue(kvp.Value));
                logEvent.AddOrUpdateProperty(property);
            }
        }
    }
}