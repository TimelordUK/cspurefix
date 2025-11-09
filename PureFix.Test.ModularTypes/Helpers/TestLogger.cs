using PureFix.Types;

namespace PureFix.Test.ModularTypes.Helpers
{
    internal class TestLogger : ILogger
    {
        public enum LogFormatTypes
        {
            App,
            Plain
        }
        public LogTrace Log { get; set; }
        public IReadOnlyList<string> Entries()
        {
            return Log.GetEntries();
        }
        private string Me { get; }
        private IFixClock Clock { get; }
        private LogFormatTypes LogFormat { get; set; }
        public TestLogger(string name, LogTrace file, LogFormatTypes format, IFixClock clock)
        {
            LogFormat = format;
            Me = name;
            Clock = clock;
            Log = file;
        }


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
                    Log.Add($"{level}:[{Me,-25}] [{Clock.Current.ToLongTimeString()}] [{Environment.CurrentManagedThreadId}] {msg}");
                    break;

                default:
                    Log.Add(msg);
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Me} [{Log.Count}] {Log.LastOrDefault()}";
        }
    }
}
