using Serilog.Events;
using Serilog;
using Serilog.Exceptions;
using System.Reflection;
using FarmPoint.Application.Common;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;

namespace WebUI.Extensions;

public static class LoggingConfigurationExtensions
{
    public static IApplicationBuilder UseCustomLogging(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.GetLevel = ExcludeHealthChecks;
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestUrl", httpContext.Request.GetDisplayUrl());
                diagnosticContext.Set("ClientIp", httpContext.Connection.RemoteIpAddress);
            };
            options.MessageTemplate += ", url {RequestUrl}, client ip {ClientIp}, correlation id {CorrelationId}";
        });
        return app;
    }

    public static LogEventLevel ExcludeHealthChecks(HttpContext context, double _, Exception? exception)
    {
        if (exception is not null || context.Response.StatusCode > 499)
        {
            return LogEventLevel.Error;
        }

        if (context.Request.GetDisplayUrl().Contains("health", StringComparison.InvariantCultureIgnoreCase))
        {
            return LogEventLevel.Verbose;
        }

        return LogEventLevel.Information;
    }

    public static Serilog.ILogger CreateBootstrapLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Console(formatProvider: System.Globalization.CultureInfo.InvariantCulture)
            .CreateBootstrapLogger();
    }

    public static IHostBuilder AddCustomLogging(this IHostBuilder builder)
    {
        AssemblyName entryAssembly = Assembly.GetEntryAssembly()!.GetName();

        return builder.UseSerilog((context, services, configuration) =>
        {
            var globalSettings = services.GetRequiredService<IOptions<GlobalSettings>>().Value;

            var (logLevel, microsoftLogLevel) = GetLogLevels(globalSettings);

            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .MinimumLevel.Override("Microsoft.", microsoftLogLevel)
                .MinimumLevel.Is(logLevel)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("Version", entryAssembly.Version)
                .Enrich.WithProperty("AppName", entryAssembly.Name)
                .WriteTo.Console();

        });
    }

    private static (LogEventLevel logLevel, LogEventLevel microsoftLogLevel) GetLogLevels(GlobalSettings globalSettings)
    {
        var logLevel = LogEventLevel.Information;
        var microsoftLevel = LogEventLevel.Warning;

        if (Enum.TryParse<LogEventLevel>(globalSettings.LogLevel, out var parsedLevel))
        {
            logLevel = parsedLevel;
        }

        if (logLevel is LogEventLevel.Warning or LogEventLevel.Error or LogEventLevel.Fatal)
        {
            microsoftLevel = parsedLevel;
        }

        return (logLevel, microsoftLevel);
    }
}
