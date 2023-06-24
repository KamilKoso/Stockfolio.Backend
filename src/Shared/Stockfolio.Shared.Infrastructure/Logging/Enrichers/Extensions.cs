using Serilog;
using Serilog.Configuration;

namespace Stockfolio.Shared.Infrastructure.Logging.Enrichers;

internal static class Extensions
{
    public static LoggerConfiguration WithDiagnosticEnricher(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        return enrichmentConfiguration.With<DiagnosticEnricher>();
    }
}