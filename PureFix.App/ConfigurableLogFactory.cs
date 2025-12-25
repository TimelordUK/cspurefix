using PureFix.Types;
using PureFix.Types.Config;
using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace PureFix.ConsoleApp;

/// <summary>
/// A log factory that creates loggers based on LoggingConfig.
/// Uses Serilog for all logging.
/// </summary>
public class ConfigurableLogFactory : ILogFactory
{
    private const string AppTemplate = "[{Timestamp:HH:mm:ss.fff zzz}] [{Name}] [{Level:u3}] [{ThreadId}] {Message:lj}{NewLine}{Exception}";
    private const string PlainTemplate = "{Message:lj}{NewLine}";

    private readonly ILogger _appLogger;
    private readonly ILogger _plainLogger;

    /// <summary>
    /// Creates a log factory with default configuration (console only).
    /// </summary>
    public ConfigurableLogFactory() : this(new LoggingConfig())
    {
    }

    /// <summary>
    /// Creates a log factory from LoggingConfig.
    /// </summary>
    public ConfigurableLogFactory(LoggingConfig config)
    {
        var level = ParseLevel(config.MinimumLevel);
        _appLogger = BuildLogger(config.AppLog, level, AppTemplate, withMetadata: true);
        _plainLogger = BuildLogger(config.FixLog, level, PlainTemplate, withMetadata: false);
    }

    /// <summary>
    /// Creates a log factory with a file prefix for both logs.
    /// This is a convenience constructor for backward compatibility.
    /// </summary>
    public ConfigurableLogFactory(string filePrefix) : this(new LoggingConfig
    {
        AppLog = new LogOutputConfig
        {
            Console = true,
            File = new FileLogConfig { Path = $"logs/{filePrefix}-app.log" }
        },
        FixLog = new LogOutputConfig
        {
            Console = true,
            File = new FileLogConfig { Path = $"logs/{filePrefix}-fix.log" }
        }
    })
    {
    }

    private static ILogger BuildLogger(LogOutputConfig config, LogEventLevel level, string template, bool withMetadata)
    {
        var logConfig = new LoggerConfiguration()
            .MinimumLevel.Is(level);

        if (config.Console)
        {
            logConfig.WriteTo.Console(outputTemplate: template);
        }

        if (config.File != null)
        {
            var interval = ParseRollingInterval(config.File.RollingInterval);
            logConfig.WriteTo.File(
                config.File.Path,
                outputTemplate: template,
                rollingInterval: interval,
                retainedFileCountLimit: config.File.RetainedFileCountLimit,
                fileSizeLimitBytes: config.File.FileSizeLimitBytes,
                rollOnFileSizeLimit: config.File.RollOnFileSizeLimit,
                flushToDiskInterval: withMetadata ? null : TimeSpan.Zero);
        }

        if (withMetadata)
        {
            logConfig.Enrich.WithThreadId();
        }

        return logConfig.CreateLogger();
    }

    private static LogEventLevel ParseLevel(string level)
    {
        return level.ToLowerInvariant() switch
        {
            "verbose" => LogEventLevel.Verbose,
            "debug" => LogEventLevel.Debug,
            "information" or "info" => LogEventLevel.Information,
            "warning" or "warn" => LogEventLevel.Warning,
            "error" => LogEventLevel.Error,
            "fatal" => LogEventLevel.Fatal,
            _ => LogEventLevel.Information
        };
    }

    private static RollingInterval ParseRollingInterval(string interval)
    {
        return interval.ToLowerInvariant() switch
        {
            "infinite" => RollingInterval.Infinite,
            "year" => RollingInterval.Year,
            "month" => RollingInterval.Month,
            "day" => RollingInterval.Day,
            "hour" => RollingInterval.Hour,
            "minute" => RollingInterval.Minute,
            _ => RollingInterval.Day
        };
    }

    public Types.ILogger MakeLogger(string name)
    {
        return new SerilogAdapter(_appLogger.ForContext("Name", name));
    }

    public Types.ILogger MakePlainLogger(string name)
    {
        return new SerilogAdapter(_plainLogger.ForContext("Name", name));
    }

    private sealed class SerilogAdapter : Types.ILogger
    {
        private readonly ILogger _logger;

        public SerilogAdapter(ILogger logger)
        {
            _logger = logger;
        }

        public void Debug(string messageTemplate) => _logger.Debug(messageTemplate);
        public void Error(Exception ex) => _logger.Error(ex.ToString());
        public void Info(string messageTemplate) => _logger.Information(messageTemplate);
        public void Warn(string messageTemplate) => _logger.Warning(messageTemplate);
    }
}
