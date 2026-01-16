using PureFix.Types;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace PureFix.ConsoleApp
{
    public class ConsoleLogFactory : ILogFactory
    {
        private const string _appTemplate = "[{Timestamp:HH:mm:ss.fff zzz}] [{Name}] [{Level:u3}] [{ThreadId}] {Message:lj}{NewLine}{Exception}";
        private const string _fixTemplate = "{Message:lj}{NewLine}";
        public enum LogFormatTypes
        {
            App,
            Plain
        }

        public string FileNamePrefix { get; set; }
        public int MaxSizeBytes { get; set; } = 524288000;

        public ILogger AppLogger { get; set; }
        public ILogger PlainLogger { get; set; }

        public ConsoleLogFactory(string filePrefix)
        {
            FileNamePrefix = filePrefix;
            AppLogger = MakeApp();
            PlainLogger = MakePlain();
        }

        private class Logger : Types.ILogger
        {
            private readonly ILogger _logger;

            public Logger(ILogger logger)
            {
                _logger = logger;
            }

            public bool IsEnabled(Types.LogLevel level) => _logger.IsEnabled(ToSerilog(level));

            public void Debug(string message) => _logger.Debug(message);
            public void Debug<T>(string template, T arg) => _logger.Debug(template, arg);
            public void Debug<T1, T2>(string template, T1 arg1, T2 arg2) => _logger.Debug(template, arg1, arg2);
            public void Debug<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3) => _logger.Debug(template, arg1, arg2, arg3);
            public void DebugV(string template, params object?[] args) => _logger.Debug(template, args);

            public void Info(string message) => _logger.Information(message);
            public void Info<T>(string template, T arg) => _logger.Information(template, arg);
            public void Info<T1, T2>(string template, T1 arg1, T2 arg2) => _logger.Information(template, arg1, arg2);
            public void Info<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3) => _logger.Information(template, arg1, arg2, arg3);
            public void InfoV(string template, params object?[] args) => _logger.Information(template, args);

            public void Warn(string message) => _logger.Warning(message);
            public void Warn<T>(string template, T arg) => _logger.Warning(template, arg);
            public void Warn<T1, T2>(string template, T1 arg1, T2 arg2) => _logger.Warning(template, arg1, arg2);
            public void Warn<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3) => _logger.Warning(template, arg1, arg2, arg3);
            public void WarnV(string template, params object?[] args) => _logger.Warning(template, args);

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

        private ILogger MakeApp()
        {
            var l = new LoggerConfiguration()
              .WriteTo.Console(outputTemplate: _appTemplate)
              .Enrich.WithThreadId()
              .WriteTo.File($"logs/{FileNamePrefix}-app-log.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: _appTemplate,
                fileSizeLimitBytes: MaxSizeBytes,
                retainedFileCountLimit: 21,
                rollOnFileSizeLimit: true)
              .CreateLogger();
             
            return l;
        }
        //  .ForContext("Name", name);
        private ILogger MakePlain()
        {
            var l = new LoggerConfiguration()
              .WriteTo.Console(outputTemplate: _fixTemplate).
                WriteTo.File($"logs/{FileNamePrefix}-fix-log.txt",
                outputTemplate: _fixTemplate,
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: MaxSizeBytes,
                flushToDiskInterval: TimeSpan.Zero,
                retainedFileCountLimit: 21,
                rollOnFileSizeLimit: true)
              .CreateLogger();
            return l;
        }

        public Types.ILogger MakeLogger(string name)
        {
            return new Logger(AppLogger.ForContext("Name", name));
        }

        public Types.ILogger MakePlainLogger(string name)
        {
            return new Logger(PlainLogger.ForContext("Name", name));
        }
    }
}
