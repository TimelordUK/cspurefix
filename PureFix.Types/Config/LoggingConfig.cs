namespace PureFix.Types.Config;

/// <summary>
/// Configuration for logging output.
/// </summary>
public class LoggingConfig
{
    /// <summary>
    /// Logging provider to use. Currently only "serilog" is supported.
    /// </summary>
    public string Provider { get; set; } = "serilog";

    /// <summary>
    /// Configuration for application event logs (formatted with timestamps, levels, etc.)
    /// </summary>
    public LogOutputConfig AppLog { get; set; } = new();

    /// <summary>
    /// Configuration for raw FIX message logs (plain text, no formatting)
    /// </summary>
    public LogOutputConfig FixLog { get; set; } = new();

    /// <summary>
    /// Minimum log level: Verbose, Debug, Information, Warning, Error, Fatal
    /// </summary>
    public string MinimumLevel { get; set; } = "Information";
}

/// <summary>
/// Configuration for a single log output (app or FIX).
/// </summary>
public class LogOutputConfig
{
    /// <summary>
    /// Whether to write to console.
    /// </summary>
    public bool Console { get; set; } = true;

    /// <summary>
    /// File logging configuration. Null to disable file logging.
    /// </summary>
    public FileLogConfig? File { get; set; }
}

/// <summary>
/// Configuration for file-based logging.
/// </summary>
public class FileLogConfig
{
    /// <summary>
    /// Path to log file. Supports {Date} placeholder for rolling files.
    /// Example: "logs/app-{Date}.log"
    /// </summary>
    public string Path { get; set; } = "logs/app.log";

    /// <summary>
    /// Rolling interval: Infinite, Year, Month, Day, Hour, Minute
    /// </summary>
    public string RollingInterval { get; set; } = "Day";

    /// <summary>
    /// Maximum number of log files to retain.
    /// </summary>
    public int RetainedFileCountLimit { get; set; } = 21;

    /// <summary>
    /// Maximum file size in bytes before rolling.
    /// </summary>
    public long FileSizeLimitBytes { get; set; } = 524288000; // 500MB

    /// <summary>
    /// Whether to roll when file size limit is reached.
    /// </summary>
    public bool RollOnFileSizeLimit { get; set; } = true;
}
