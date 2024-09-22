using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    public class TestLogger : ILogger
    {
        public enum LogFormatTypes
        {
            App,
            Plain
        }
        public IReadOnlyList<string> Entries => _log;
        private string Me { get; }
        private IFixClock Clock { get; }
        private LogFormatTypes LogFormat { get; set; }
        public TestLogger(string name, LogFormatTypes format, IFixClock clock)
        {
            LogFormat = format;
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
            switch (LogFormat)
            {
                case LogFormatTypes.App:
                    _log.Add($"{level}:[{Me}] {Clock.Current.ToLongTimeString()} {Environment.CurrentManagedThreadId} {msg}");
                    break;

                default:
                    _log.Add(msg);
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Me} [{_log.Count}] {_log.LastOrDefault()}";
        }
    }
}
