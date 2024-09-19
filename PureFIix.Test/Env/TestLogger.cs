using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    public class TestLogger : ILogFactory
    {
        private readonly IFixClock _clock;
        public TestLogger(IFixClock? clock = null)
        {
            _clock = clock ?? new RealtimeClock();
        }

        private class Logger : ILogger
        {
            private string Me { get; }
            private IFixClock Clock { get; }
            public Logger(string name, IFixClock clock)
            {
                Me = name;
                Clock = clock;
            }

            private List<string> _log = [];
            public void Info(string messageTemplate)
            {
                AddEntry("I", messageTemplate);
            }

            public void Warn(string messageTemplate)
            {
                AddEntry("W", messageTemplate);
            }

            public void Debug(string messageTemplate)
            {
                AddEntry("D", messageTemplate);
            }

            public void Error(Exception ex)
            {
                AddEntry("E", ex.ToString());
            }

            private void AddEntry(string level, string msg)
            {
                _log.Add($"{level}:[{Me}] {Clock.Current.ToLongTimeString()} {Environment.CurrentManagedThreadId} {msg}");
            }
        }

        public ILogger MakeLogger(string name)
        {
            return new Logger(name, _clock);
        }
    }
}
