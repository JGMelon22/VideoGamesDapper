using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace VideoGamesDapper.Configuration;

public static class OpenTelemetryCollection
{
    public static void AddMetrics(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenTelemetry().WithMetrics((options) =>
        {
            options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("VideogamesDapper"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation()
            .AddOtlpExporter(otel =>
            {
                otel.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                otel.Endpoint = new Uri(configuration["Otlp:Endpoint"]!);
            });
        });
    }

    public static void AddTracing(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenTelemetry().WithTracing((options) =>
        {
            options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("VideogamesDapper"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(otel =>
            {
                otel.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                otel.Endpoint = new Uri(configuration["Otlp:Endpoint"]!);
            });
        });
    }
}
