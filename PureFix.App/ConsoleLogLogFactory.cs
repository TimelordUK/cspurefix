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
        const string _appTemplate = "[{Timestamp:HH:mm:ss.fff}] [{Name}] [{Level:u3}] [{ThreadId}] {Message:lj}{NewLine}{Exception}";
        const string _fixTemplate = "{Message:lj}{NewLine}";
        public enum LogFormatTypes
        {
            App,
            Plain
        }

        private class Logger : Types.ILogger
        {
            ILogger _logger;
           
            public Logger(string name, ILogger logger)
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
              .CreateLogger()
              .ForContext("Name", name);
            return l;
        }

        private ILogger MakePlain(string name)
        {
            var l = new LoggerConfiguration()
              .WriteTo.Console(outputTemplate: _fixTemplate)
              .CreateLogger();
            return l;
        }

        public Types.ILogger MakeLogger(string name)
        {
            return new Logger(name, MakeApp(name));
        }

        public Types.ILogger MakePlainLogger(string name)
        {
            return new Logger(name, MakePlain(name));
        }
    }
}
