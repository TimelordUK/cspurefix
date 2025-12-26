using PureFix.Types;
using PureFix.Types.Config;
using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace PureFix.Examples.Shared;

/// <summary>
/// A log factory that creates loggers based on LoggingConfig.
/// Uses Serilog for all logging.
/// </summary>
public class ConsoleLogFactory : ILogFactory
{
    private const string AppTemplate = "[{Timestamp:HH:mm:ss.fff zzz}] [{Name}] [{Level:u3}] [{ThreadId}] {Message:lj}{NewLine}{Exception}";
    private const string PlainTemplate = "{Message:lj}{NewLine}";

    private readonly ILogger _appLogger;
    private readonly ILogger _plainLogger;

    /// <summary>
    /// Creates a log factory with default configuration (console only).
    /// </summary>
    public ConsoleLogFactory() : this((LoggingConfig?)null)
    {
    }

    /// <summary>
    /// Creates a log factory from session description (reads Logging config).
    /// </summary>
    public ConsoleLogFactory(ISessionDescription? description) : this(description?.Logging)
    {
    }

    /// <summary>
    /// Creates a log factory from LoggingConfig.
    /// </summary>
    public ConsoleLogFactory(LoggingConfig? config)
    {
        config ??= new LoggingConfig();
        var level = ParseLevel(config.MinimumLevel);
        _appLogger = BuildLogger(config.AppLog, level, AppTemplate, withMetadata: true);
        _plainLogger = BuildLogger(config.FixLog, level, PlainTemplate, withMetadata: false);
    }

    /// <summary>
    /// Creates a log factory with a file prefix for both logs.
    /// This is a convenience constructor for backward compatibility.
    /// </summary>
    public ConsoleLogFactory(string filePrefix) : this(new LoggingConfig
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

    /// <summary>
    /// Legacy constructor for backward compatibility.
    /// </summary>
    [Obsolete("Use constructor with LoggingConfig or ISessionDescription")]
    public ConsoleLogFactory(IFixClock clock) : this((LoggingConfig?)null)
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

        public bool IsEnabled(Types.LogLevel level) => _logger.IsEnabled(ToSerilog(level));

        public void Debug(string message) => _logger.Debug(message);
        public void Debug<T>(string template, T arg) => _logger.Debug(template, arg);
        public void Debug<T1, T2>(string template, T1 arg1, T2 arg2) => _logger.Debug(template, arg1, arg2);
        public void Debug<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3) => _logger.Debug(template, arg1, arg2, arg3);

        public void Info(string message) => _logger.Information(message);
        public void Info<T>(string template, T arg) => _logger.Information(template, arg);
        public void Info<T1, T2>(string template, T1 arg1, T2 arg2) => _logger.Information(template, arg1, arg2);
        public void Info<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3) => _logger.Information(template, arg1, arg2, arg3);

        public void Warn(string message) => _logger.Warning(message);
        public void Warn<T>(string template, T arg) => _logger.Warning(template, arg);
        public void Warn<T1, T2>(string template, T1 arg1, T2 arg2) => _logger.Warning(template, arg1, arg2);
        public void Warn<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3) => _logger.Warning(template, arg1, arg2, arg3);

        public void Error(Exception ex, string? message = null) => _logger.Error(ex, message ?? ex.Message);
        public void Error<T>(Exception ex, string template, T arg) => _logger.Error(ex, template, arg);
        public void Error<T1, T2>(Exception ex, string template, T1 arg1, T2 arg2) => _logger.Error(ex, template, arg1, arg2);

        private static LogEventLevel ToSerilog(Types.LogLevel level) => level switch
        {
            Types.LogLevel.Debug => LogEventLevel.Debug,
            Types.LogLevel.Info => LogEventLevel.Information,
            Types.LogLevel.Warn => LogEventLevel.Warning,
            Types.LogLevel.Error => LogEventLevel.Error,
            _ => LogEventLevel.Information
        };
    }
}
