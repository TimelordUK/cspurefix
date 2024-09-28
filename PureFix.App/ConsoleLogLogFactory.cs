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
        private class Logger : Types.ILogger
        {
            ILogger _logger;
            public Logger()
            {
                _logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();
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
        public Types.ILogger MakeLogger(string name)
        {
            return new Logger();
        }

        public Types.ILogger MakePlainLogger(string name)
        {
            return new Logger();
        }
    }
}
