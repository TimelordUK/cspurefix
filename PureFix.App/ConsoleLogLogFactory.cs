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
        const string _appTemplate = "[{Timestamp:HH:mm:ss.fff zzz}] [{Name}] [{Level:u3}] [{ThreadId}] {Message:lj}{NewLine}{Exception}";
        const string _fixTemplate = "{Message:lj}{NewLine}";
        public enum LogFormatTypes
        {
            App,
            Plain
        }

        public string FileName { get; set; }
        public int MaxSizeBytes { get; set; } = 524288000;

        public ILogger AppLogger { get; set; }
        public ILogger PlainLogger { get; set; }

        public ConsoleLogFactory(string file)
        {
            FileName = file;
            AppLogger = MakeApp(file);
            PlainLogger = MakePlain(file);
        }

        private class Logger : Types.ILogger
        {
            ILogger _logger;
           
            public Logger(ILogger logger)
            {
                _logger = logger;
            }
            public void Debug(string messageTemplate)
            {
                _logger.Debug(messageTemplate);
            }

            public void Error(Exception ex)
            {
                _logger.Error(ex.ToString());
            }

            public void Info(string messageTemplate)
            {
                _logger.Information(messageTemplate);
            }

            public void Warn(string messageTemplate)
            {
                _logger.Warning(messageTemplate);
            }
        }

        private ILogger MakeApp(string name)
        {
            var l = new LoggerConfiguration()
              .WriteTo.Console(outputTemplate: _appTemplate)
              .Enrich.WithThreadId()
              .WriteTo.File($"logs/{FileName}-app-log.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: _appTemplate,
                fileSizeLimitBytes: MaxSizeBytes,
                retainedFileCountLimit: 21,
                rollOnFileSizeLimit: true)
              .CreateLogger();
             
            return l;
        }
        //  .ForContext("Name", name);
        private ILogger MakePlain(string name)
        {
            var l = new LoggerConfiguration()
              .WriteTo.Console(outputTemplate: _fixTemplate).
                WriteTo.File($"logs/{FileName}-fix-log.txt",
                outputTemplate: _fixTemplate,
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: MaxSizeBytes,
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
